namespace TopUp.Domain;

public class TopUp : Entity<TopUpId>
{
    public static TopUp Create(UserId userId, BeneficiaryId beneficiaryId, TopUpAmount amount, decimal fee)
    {
        var topUp = new TopUp
        {
            Id = TopUpId.From(Guid.NewGuid()),
            UserId = userId,
            BeneficiaryId = beneficiaryId,
            Amount = amount,
            Fee = fee,
        };

        return topUp;
    }

    // internal TopUp(UserId userId, BeneficiaryId beneficiaryId, TopUpAmount amount, decimal fee)
    // {
    //     Id = TopUpId.From(Guid.NewGuid());
    //     UserId = userId;
    //     BeneficiaryId = beneficiaryId;
    //     Amount = amount;
    //     Fee = fee;
    // }

    public BeneficiaryId BeneficiaryId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public TopUpAmount Amount { get; private set; } = default!;
    public decimal Fee { get; private set; }
    public decimal TotalAmount
    {
        get => Amount.Value + Fee;
        private set { }
    }
}
