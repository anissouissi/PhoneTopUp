namespace BankAccount.Domain;

public record Balance
{
    public static Balance From(decimal value)
    {
        return new Balance(value);
    }

    private Balance(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public bool IsLessThan(TransactionAmount amount)
    {
        return Money.From(Value).IsLessThan(amount.MoneyValue);
    }

    public Balance Subtract(TransactionAmount amount)
    {
        var result = Money.From(Value).Subtract(amount.MoneyValue);
        return From(result.Value);
    }

    public Balance Add(TransactionAmount amount)
    {
        var result = Money.From(Value).Add(amount.MoneyValue);
        return From(result.Value);
    }
}