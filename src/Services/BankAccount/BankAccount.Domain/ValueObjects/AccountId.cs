using BuildingBlocks;

namespace BankAccount.Domain;

public record AccountId
{
    public Guid Value { get; }
    private AccountId(Guid value) => Value = value;
    public static AccountId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.AccountIdEmpty);
        }

        return new AccountId(value);
    }
}
