using BuildingBlocks;

namespace TopUp.Domain;

public record AccountNumber
{
    public Guid Value { get; }
    private AccountNumber(Guid value) => Value = value;
    public static AccountNumber From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.AccountNumberEmpty);
        }

        return new AccountNumber(value);
    }
}
