namespace TopUp.Application;

public record TopUpDto(Guid BeneficiaryId, decimal Amount, decimal Fee);
