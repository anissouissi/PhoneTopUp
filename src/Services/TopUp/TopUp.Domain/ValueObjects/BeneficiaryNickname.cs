using BuildingBlocks;

namespace TopUp.Domain;

public record BeneficiaryNickname
{
    public string Value { get; }
    private BeneficiaryNickname(string value) => Value = value;
    public static BeneficiaryNickname From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException(ValidationMessages.NicknameEmpty);
        }
        if (value.Length > DomainConstants.NicknameMaxLength)
        {
            throw new ValidationException(string.Format(ValidationMessages.NicknameMaxLength,
                DomainConstants.NicknameMaxLength));
        }

        return new BeneficiaryNickname(value);
    }
}
