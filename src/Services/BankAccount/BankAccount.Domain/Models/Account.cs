namespace BankAccount.Domain;

public class Account : Aggregate<AccountId>
{
    public AccountNumber AccountNumber { get; private set; } = default!;
    public HolderId HolderId { get; private set; } = default!;
    public Balance Balance { get; private set; } = default!;

    public static Account Create(AccountId id, AccountNumber number, HolderId holderId, Balance balance)
    {
        var account = new Account
        {
            Id = id,
            AccountNumber = number,
            HolderId = holderId,
            Balance = balance
        };

        account.AddDomainEvent(new AccountCreatedEvent(account));

        return account;
    }

    public void Debit(TransactionAmount amount)
    {
        if (Balance.IsLessThan(amount))
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance = Balance.Subtract(amount);
    }

    public void Credit(TransactionAmount amount)
    {
        Balance = Balance.Add(amount);
    }
}
