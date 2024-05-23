using BuildingBlocks;

namespace BankAccount.Domain;

public record Email
{
    public string Value { get; }

    private Email(string value) => Value = value.Trim();

    public static Email From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException(ValidationMessages.EmailEmpty);
        }

        if (!RegexPatterns.EmailIsValid.IsMatch(value))
        {
            throw new ValidationException(ValidationMessages.EmailInvalid);
        }

        return new Email(value);
    }
}
