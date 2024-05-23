using BuildingBlocks;

namespace BankAccount.Domain;

public record Address
{
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;
    protected Address()
    {
    }

    private Address(string addressLine, string country, string state, string zipCode)
    {
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address From(string addressLine, string country, string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(addressLine))
        {
            throw new ValidationException(ValidationMessages.AddressEmpty);
        }
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new ValidationException(ValidationMessages.CountryEmpty);
        }
        if (string.IsNullOrWhiteSpace(state))
        {
            throw new ValidationException(ValidationMessages.StateEmpty);
        }
        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new ValidationException(ValidationMessages.ZipCodeEmpty);
        }

        return new Address(addressLine, country, state, zipCode);
    }
}
