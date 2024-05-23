namespace BankAccount.Domain;

public record Money
{
    public static Money From(decimal value)
    {
        return new Money(value);
    }

    private Money(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public Money Subtract(Money money)
    {
        var result = Value - money.Value;
        return From(result);
    }

    public Money Add(Money money)
    {
        var result = Value + money.Value;
        return From(result);
    }

    public bool IsNegative()
    {
        return Value < 0;
    }

    public bool IsZeroOrNegative()
    {
        return Value <= 0;
    }

    public bool IsLessThan(Money other)
    {
        return Value < other.Value;
    }

}