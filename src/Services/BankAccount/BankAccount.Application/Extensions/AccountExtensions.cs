using BankAccount.Domain;

namespace BankAccount.Application;

public static class AccountExtensions
{
    public static IEnumerable<AccountDto> ToAccountDtoList(this IEnumerable<Account> accounts)
    {
        return accounts.Select(Account => new AccountDto(
            Id: Account.Id.Value,
            HolderId: Account.HolderId.Value,
            AccountNumber: Account.AccountNumber.Value,
            Balance: Account.Balance.Value
        ));
    }

    public static AccountDto ToAccountDto(this Account Account)
    {
        return DtoFromAccount(Account);
    }

    private static AccountDto DtoFromAccount(Account Account)
    {
        return new AccountDto(
                    Id: Account.Id.Value,
                    HolderId: Account.HolderId.Value,
                    AccountNumber: Account.AccountNumber.Value,
                    Balance: Account.Balance.Value
                );
    }
}
