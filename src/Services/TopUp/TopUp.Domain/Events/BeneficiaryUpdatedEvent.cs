namespace TopUp.Domain;

public record BeneficiaryUpdatedEvent(Beneficiary Beneficiary) : IDomainEvent;
