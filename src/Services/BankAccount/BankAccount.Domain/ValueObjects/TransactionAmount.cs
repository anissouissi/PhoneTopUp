using BuildingBlocks;

namespace BankAccount.Domain;

public record TransactionAmount
{
    public static TransactionAmount From(Money value)
    {
        return new TransactionAmount(value);
    }

    public static TransactionAmount From(decimal amount)
    {
        return From(Money.From(amount));
    }

    private TransactionAmount(Money value)
    {
        if (value.IsZeroOrNegative())
        {
            throw new ValidationException(ValidationMessages.AmountNotPositive);
        }
        MoneyValue = value;
    }

    public Money MoneyValue { get; }
}
