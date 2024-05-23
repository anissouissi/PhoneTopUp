using BuildingBlocks;

namespace TopUp.Domain;

public record BeneficiaryId
{
    public Guid Value { get; }
    private BeneficiaryId(Guid value) => Value = value;
    public static BeneficiaryId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ValidationException(ValidationMessages.BeneficiaryIdEmpty);
        }

        return new BeneficiaryId(value);
    }
}
