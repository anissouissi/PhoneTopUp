using BuildingBlocks;

namespace TopUp.Domain;

public record UserId
{
    public Guid Value { get; }
    private UserId(Guid value) => Value = value;
    public static UserId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.UserIdEmpty);
        }

        return new UserId(value);
    }
}
