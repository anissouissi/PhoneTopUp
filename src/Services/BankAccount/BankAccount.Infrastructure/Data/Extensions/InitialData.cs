using BankAccount.Domain;

namespace BankAccount.Infrastructure;
internal class InitialData
{
    public static IEnumerable<Holder> Holders =>
    [
        Holder.Create(HolderId.From(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
            HolderName.From("anis","souissi"),
            Email.From("test@example.com"),
            Phone.From("+971521111111"),
            Address.From("test","UAE","Dubai","00000")
        ),
        Holder.Create(HolderId.From(new Guid("f2304676-2b68-4fcd-b699-3142721708a9")),
            HolderName.From("ali","ala"),
            Email.From("ali@example.com"),
            Phone.From("+971521111111"),
            Address.From("test","UAE","Dubai","00000")
        ),
    ];

    public static IEnumerable<Account> Accounts =>
    [
        Account.Create(AccountId.From(Guid.NewGuid()),
            AccountNumber.From(new Guid("e02fa0e4-01ad-090A-c130-0d05a0008ba0")),
            HolderId.From(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
            Balance.From(10000)
        ),
        Account.Create(AccountId.From(Guid.NewGuid()),
            AccountNumber.From(new Guid("d2719e13-cc0a-4d15-8b56-8e1a1f7a3b5e")),
            HolderId.From(new Guid("f2304676-2b68-4fcd-b699-3142721708a9")),
            Balance.From(10)
        ),
    ];
}
