using BuildingBlocks;

namespace TopUp.Domain;

public record TopUpId
{
    public Guid Value { get; }
    private TopUpId(Guid value) => Value = value;
    public static TopUpId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.TopUpIdEmpty);
        }

        return new TopUpId(value);
    }
}
