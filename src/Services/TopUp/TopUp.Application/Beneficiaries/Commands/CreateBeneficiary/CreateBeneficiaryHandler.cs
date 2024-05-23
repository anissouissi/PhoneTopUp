using BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using TopUp.Domain;

namespace TopUp.Application;
public class CreateBeneficiaryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateBeneficiaryCommand, CreateBeneficiaryResult>
{
    public async Task<CreateBeneficiaryResult> Handle(CreateBeneficiaryCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(u => u.Beneficiaries)
            .FirstOrDefaultAsync(u => u.Id == UserId.From(command.Beneficiary.UserId), cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(command.Beneficiary.UserId);
        }

        var beneficiary = CreateNewBeneficiary(command.Beneficiary);

        user.AddBeneficiary(beneficiary);

        dbContext.Beneficiaries.Add(beneficiary);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBeneficiaryResult(beneficiary.Id.Value);
    }

    private static Beneficiary CreateNewBeneficiary(CreateBeneficiaryDto beneficiary)
    {
        var newBeneficiary = Beneficiary.Create(
                id: BeneficiaryId.From(Guid.NewGuid()),
                userId: UserId.From(beneficiary.UserId),
                nickname: BeneficiaryNickname.From(beneficiary.BeneficiaryNickname),
                phone: Phone.From(beneficiary.Phone)
                );

        return newBeneficiary;
    }
}
