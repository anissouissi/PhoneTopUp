using TopUp.Domain;

namespace TopUp.Application;
public static class BeneficiaryExtensions
{
    public static IEnumerable<BeneficiaryDto> ToBeneficiaryDtoList(this IEnumerable<Beneficiary> beneficiaries)
    {
        return beneficiaries.Select(beneficiary => new BeneficiaryDto(
            Id: beneficiary.Id.Value,
            UserId: beneficiary.UserId.Value,
            BeneficiaryNickname: beneficiary.Nickname.Value,
            Phone: beneficiary.Phone.Value,
            TopUps: beneficiary.TopUps.Select(t => new TopUpDto(t.BeneficiaryId.Value, t.Amount, t.Fee)).ToList()
        ));
    }

    public static BeneficiaryDto ToBeneficiaryDto(this Beneficiary beneficiary)
    {
        return DtoFromBeneficiary(beneficiary);
    }

    private static BeneficiaryDto DtoFromBeneficiary(Beneficiary beneficiary)
    {
        return new BeneficiaryDto(
                    Id: beneficiary.Id.Value,
                    UserId: beneficiary.UserId.Value,
                    BeneficiaryNickname: beneficiary.Nickname.Value,
                    Phone: beneficiary.Phone.Value,
                    TopUps: beneficiary.TopUps.Select(t => new TopUpDto(t.BeneficiaryId.Value, t.Amount, t.Fee)).ToList()
                );
    }
}
