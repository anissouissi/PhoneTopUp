using BuildingBlocks;

namespace TopUp.Domain;

public record Phone
{
    public string Value { get; }

    private Phone(string value) => Value = value.Trim().Replace(" ", "");

    public static Phone From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException(ValidationMessages.PhoneEmpty);
        }

        if (!RegexPatterns.PhoneIsValid.IsMatch(value))
        {
            throw new ValidationException(ValidationMessages.PhoneInvalid);
        }

        return new Phone(value);
    }
}
