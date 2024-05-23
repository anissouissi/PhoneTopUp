using BuildingBlocks;

namespace TopUp.Domain;

public class Beneficiary : Aggregate<BeneficiaryId>
{
    public BeneficiaryNickname Nickname { get; private set; } = default!;
    public Phone Phone { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    private readonly List<TopUp> _topUps = [];
    public IReadOnlyList<TopUp> TopUps => _topUps.AsReadOnly();
    public decimal TotalTopUp
    {
        get => TopUps.Sum(t => t.Amount.Value);
    }
    public decimal CurrentTotalTopUpPerMonth
    {
        get
        {
            if (_topUps.Count == 0)
            {
                return 0;
            }

            var lastTopUpDate = _topUps.OrderByDescending(t => t.CreatedAt).First().CreatedAt;
            var lastMonthTopUps = _topUps.Where(t => t.CreatedAt > lastTopUpDate.GetValueOrDefault().AddDays(-30));
            return lastMonthTopUps.Sum(t => t.Amount.Value);
        }
    }


    public static Beneficiary Create(BeneficiaryId id, BeneficiaryNickname nickname, Phone phone, UserId userId)
    {
        var beneficiary = new Beneficiary
        {
            Id = id,
            Nickname = nickname,
            Phone = phone,
            UserId = userId
        };

        beneficiary.AddDomainEvent(new BeneficiaryCreatedEvent(beneficiary));

        return beneficiary;
    }

    public void Update(BeneficiaryNickname nickname, Phone phone)
    {
        Nickname = nickname;
        Phone = phone;

        AddDomainEvent(new BeneficiaryUpdatedEvent(this));
    }

    public void AddTopUp(TopUpAmount amount, decimal fee)
    {
        if (amount.Value <= 0)
        {
            throw new ValidationException(ValidationMessages.AmountNotPositive);
        }
        if (fee < 0)
        {
            throw new ValidationException(ValidationMessages.AmountNotPositive);
        }

        var topUp = TopUp.Create(UserId, Id, amount, fee);
        _topUps.Add(topUp);
    }

    public void RemoveTopUp(TopUpId topUpId)
    {
        var topUp = _topUps.FirstOrDefault(t => t.Id == topUpId);
        if (topUp is not null)
        {
            _topUps.Remove(topUp);
        }
    }
}
