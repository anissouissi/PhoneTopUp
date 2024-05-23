using BuildingBlocks;

namespace BankAccount.Domain;

public record HolderId
{
    public Guid Value { get; }
    private HolderId(Guid value) => Value = value;
    public static HolderId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.HolderIdEmpty);
        }

        return new HolderId(value);
    }
}
