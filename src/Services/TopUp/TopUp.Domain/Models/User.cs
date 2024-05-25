namespace TopUp.Domain;

public class User : Aggregate<UserId>
{
    public UserNickname Nickname { get; set; } = default!;
    public Phone Phone { get; set; } = default!;
    public AccountNumber AccountNumber { get; set; } = default!;
    public bool Verified { get; set; }

    private readonly List<Beneficiary> _beneficiaries = [];
    public IReadOnlyList<Beneficiary> Beneficiaries => _beneficiaries.AsReadOnly();

    public static User Create(UserId id, string nickname, string phoneNumber, AccountNumber accountNumber, bool verified)
    {
        var user = new User
        {
            Id = id,
            Nickname = UserNickname.From(nickname),
            Phone = Phone.From(phoneNumber),
            AccountNumber = accountNumber,
            Verified = verified
        };

        return user;
    }

    public void AddBeneficiary(Beneficiary beneficiary)
    {
        if (_beneficiaries.Count >= DomainConstants.MaxAllowedBeneficiaries)
        {
            throw new DomainException(string.Format(DomainExceptionMessages.MaxAllowedBeneficiaries, DomainConstants.MaxAllowedBeneficiaries));
        }

        _beneficiaries.Add(beneficiary);
    }

    public void RemoveBeneficiary(BeneficiaryId beneficiaryId)
    {
        var beneficiary = _beneficiaries.FirstOrDefault(b => b.Id == beneficiaryId);
        if (beneficiary is not null)
        {
            _beneficiaries.Remove(beneficiary);
        }
    }

    public void AddTopUp(BeneficiaryId beneficiaryId, TopUpAmount amount, decimal fee)
    {
        var beneficiary = _beneficiaries.FirstOrDefault(b => b.Id == beneficiaryId);

        if (beneficiary is null)
        {
            return;
        }

        var beneficiariesCurrentTotalTopUpPerMonth = _beneficiaries.Sum(b => b.CurrentTotalTopUpPerMonth);
        if (beneficiariesCurrentTotalTopUpPerMonth + amount.Value > DomainConstants.UserMaxAllowedTopUpPerMonth)
        {
            throw new DomainException(string.Format(DomainExceptionMessages.MaxAllowedTopUpPerMonth,
                    DomainConstants.UserMaxAllowedTopUpPerMonth));
        }

        var userMaxAllowedTopUpPerMonthPerBeneficiary = Verified ?
                DomainConstants.VerifiedUserMaxAllowedTopUpPerMonthPerBeneficiary :
                DomainConstants.NotVerifiedUserMaxAllowedTopUpPerMonthPerBeneficiary;

        if (beneficiary.CurrentTotalTopUpPerMonth + amount.Value > userMaxAllowedTopUpPerMonthPerBeneficiary)
        {
            throw new DomainException(string.Format(DomainExceptionMessages.MaxAllowedTopUpPerMonthPerBeneficiary,
                userMaxAllowedTopUpPerMonthPerBeneficiary));
        }

        beneficiary.AddTopUp(amount, fee);
    }
}
