using BuildingBlocks;

namespace TopUp.Domain;

public record UserNickname
{
    public string Value { get; }
    private UserNickname(string value) => Value = value;
    public static UserNickname From(string value)
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

        return new UserNickname(value);
    }

}
