using BankAccount.Domain;
using BuildingBlocks;

namespace BankAccount.Application;

public class CreateAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public async Task<CreateAccountResult> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        var holder = CreateHolder(command.Account.Holder);
        var account = CreateNewAccount(command.Account, holder.Id);

        dbContext.Holders.Add(holder);
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAccountResult(account.AccountNumber.Value);
    }

    private static Account CreateNewAccount(CreateAccountDto createAccountDto, HolderId holderId)
    {
        var newAccount = Account.Create(
                id: AccountId.From(Guid.NewGuid()),
                number: AccountNumber.From(Guid.NewGuid()),
                holderId: holderId,
                balance: Balance.From(createAccountDto.Balance));

        return newAccount;
    }

    private static Holder CreateHolder(HolderDto holder)
    {
        var newHolder = Holder.Create(id: HolderId.From(Guid.NewGuid()),
                name: HolderName.From(holder.FirstName, holder.LastName),
                email: Email.From(holder.Email),
                phone: Phone.From(holder.Phone),
                address: Address.From(holder.AddressLine, holder.Country, holder.State, holder.ZipCode));
        return newHolder;
    }
}
