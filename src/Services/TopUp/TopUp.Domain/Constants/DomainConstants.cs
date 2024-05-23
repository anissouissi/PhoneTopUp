namespace TopUp.Domain;

public static class DomainConstants
{
    public const int MaxAllowedBeneficiaries = 5;
    public const int VerifiedUserMaxAllowedTopUpPerMonthPerBeneficiary = 500;
    public const int NotVerifiedUserMaxAllowedTopUpPerMonthPerBeneficiary = 1000;
    public const int UserMaxAllowedTopUpPerMonth = 3000;
    public const int NicknameMaxLength = 20;
    public const decimal TopUpFee = 1;
}
