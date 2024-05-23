using BuildingBlocks;
using BankAccount.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Application;
public class DebitAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DebitAccountCommand, DebitAccountResult>
{
    public async Task<DebitAccountResult> Handle(DebitAccountCommand command, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == AccountNumber.From(command.Transaction.AccountNumber), cancellationToken: cancellationToken);

        if (account is null)
        {
            throw new AccountNotFoundException(command.Transaction.AccountNumber);
        }

        account.Debit(TransactionAmount.From(command.Transaction.Amount));

        dbContext.Accounts.Update(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DebitAccountResult(true);
    }
}
