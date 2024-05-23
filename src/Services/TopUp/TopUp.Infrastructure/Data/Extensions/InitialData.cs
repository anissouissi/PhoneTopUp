using TopUp.Domain;

namespace TopUp.Infrastructure;
internal class InitialData
{
    public static IEnumerable<User> Users =>
    [
        User.Create(UserId.From(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                "anis", "+971521111111", AccountNumber.From(new Guid("e02fa0e4-01ad-090A-c130-0d05a0008ba0")), true),
        User.Create(UserId.From(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                "ali", "+971522222222", AccountNumber.From(new Guid("d2719e13-cc0a-4d15-8b56-8e1a1f7a3b5e")), false),
    ];

    public static IEnumerable<Beneficiary> BeneficiariesWithTopUps
    {
        get
        {
            var beneficiary1 = Beneficiary.Create(
                            BeneficiaryId.From(Guid.NewGuid()),
                            BeneficiaryNickname.From("B1"),
                            Phone.From("+971521000001"),
                            UserId.From(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")));
            beneficiary1.AddTopUp(10, 1);
            beneficiary1.AddTopUp(20, 1);
            beneficiary1.AddTopUp(75, 1);

            var beneficiary2 = Beneficiary.Create(
                            BeneficiaryId.From(Guid.NewGuid()),
                            BeneficiaryNickname.From("B2"),
                            Phone.From("+971521000002"),
                            UserId.From(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")));
            beneficiary2.AddTopUp(10, 1);
            beneficiary2.AddTopUp(75, 1);

            var beneficiary3 = Beneficiary.Create(
                            BeneficiaryId.From(Guid.NewGuid()),
                            BeneficiaryNickname.From("B3"),
                            Phone.From("+971521000003"),
                            UserId.From(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")));
            beneficiary3.AddTopUp(5, 1);

            return [beneficiary1, beneficiary2, beneficiary3];
        }
    }
}
