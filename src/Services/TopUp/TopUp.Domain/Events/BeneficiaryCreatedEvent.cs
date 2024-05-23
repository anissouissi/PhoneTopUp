namespace TopUp.Domain;

public record BeneficiaryCreatedEvent(Beneficiary Beneficiary) : IDomainEvent;
