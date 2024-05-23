using BuildingBlocks;
using BankAccount.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Application;
public class DeleteAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteAccountCommand, DeleteAccountResult>
{
    public async Task<DeleteAccountResult> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        var accountId = AccountId.From(command.AccountId);
        var account = await dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken: cancellationToken);

        if (account is null)
        {
            throw new AccountNotFoundException(command.AccountId);
        }

        dbContext.Accounts.Remove(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteAccountResult(true);
    }
}
