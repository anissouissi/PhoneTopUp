using BuildingBlocks;
using BankAccount.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Application;
public class CreditAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreditAccountCommand, CreditAccountResult>
{
    public async Task<CreditAccountResult> Handle(CreditAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == AccountNumber.From(command.Transaction.AccountNumber), cancellationToken: cancellationToken);

        if (account is null)
        {
            throw new AccountNotFoundException(command.Transaction.AccountNumber);
        }

        account.Credit(TransactionAmount.From(command.Transaction.Amount));

        dbContext.Accounts.Update(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreditAccountResult(true);
    }
}
