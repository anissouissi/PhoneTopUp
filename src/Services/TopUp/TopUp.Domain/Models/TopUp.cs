namespace TopUp.Domain;

public class TopUp : Entity<TopUpId>
{
    internal TopUp(UserId userId, BeneficiaryId beneficiaryId, decimal amount, decimal fee)
    {
        Id = TopUpId.From(Guid.NewGuid());
        UserId = userId;
        BeneficiaryId = beneficiaryId;
        Amount = amount;
        Fee = fee;
    }

    public BeneficiaryId BeneficiaryId { get; private set; }
    public UserId UserId { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Fee { get; private set; }
    public decimal TotalAmount
    {
        get => Amount + Fee;
        private set { }
    }
}
