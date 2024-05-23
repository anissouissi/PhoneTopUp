
namespace TopUp.Domain;

public static class DomainExceptionMessages
{
    public const string MaxAllowedBeneficiaries = "User can only have a maximum of {0} beneficiaries.";
    public const string MaxAllowedTopUpPerMonthPerBeneficiary = "User can only top up a maximum of {0} per calendar month per beneficiary.";
    public const string MaxAllowedTopUpPerMonth = "User can only top up a maximum of {0} per calendar month.";
}