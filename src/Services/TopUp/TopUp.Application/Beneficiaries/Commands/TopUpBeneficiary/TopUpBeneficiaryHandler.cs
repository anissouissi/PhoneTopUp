using BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using TopUp.Domain;

namespace TopUp.Application;
public class TopUpBeneficiaryHandler(IApplicationDbContext dbContext, BankAccountServiceHttpClient bankAccountServiceHttpClient)
    : ICommandHandler<TopUpBeneficiaryCommand, TopUpBeneficiaryResult>
{
    public async Task<TopUpBeneficiaryResult> Handle(TopUpBeneficiaryCommand command, CancellationToken cancellationToken)
    {
        // check if user exists
        var userId = UserId.From(command.TopUpBeneficiary.UserId);

        // TODO optimize query: 
        // 1) get only user
        // 2) if he exists, include beneficiaries and top ups
        var user = await dbContext.Users
            .Include(u => u.Beneficiaries)
            .ThenInclude(b => b.TopUps)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(command.TopUpBeneficiary.UserId);
        }

        // check if beneficiary exists
        var beneficiaryId = BeneficiaryId.From(command.TopUpBeneficiary.BeneficiaryId);

        var beneficiary = user.Beneficiaries.FirstOrDefault(b => b.Id == beneficiaryId);
        if (beneficiary is null)
        {
            throw new BeneficiaryNotFoundException(command.TopUpBeneficiary.BeneficiaryId);
        }

        var topUpAmount = TopUpAmount.FromValue(command.TopUpBeneficiary.Amount);

        if (topUpAmount is null)
        {
            throw new AmountNotValidException($"{command.TopUpBeneficiary.Amount} is not a valid amount");
        }

        // add top up
        user.AddTopUp(beneficiaryId, topUpAmount, command.TopUpBeneficiary.Fee);

        // try to debit bank account before saving changes
        await bankAccountServiceHttpClient.DebitAccount(
            new BankAccountTransactionDto(user.AccountNumber.Value, command.TopUpBeneficiary.Amount + command.TopUpBeneficiary.Fee));

        await dbContext.SaveChangesAsync(cancellationToken);

        return new TopUpBeneficiaryResult(true);
    }
}
