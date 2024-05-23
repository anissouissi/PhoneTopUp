using BuildingBlocks;

namespace BankAccount.Domain;

public record HolderName
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    protected HolderName()
    {
    }

    private HolderName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static HolderName From(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ValidationException(ValidationMessages.FirstNameEmpty);
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ValidationException(ValidationMessages.LastNameEmpty);
        }

        return new HolderName(firstName, lastName);
    }
}
