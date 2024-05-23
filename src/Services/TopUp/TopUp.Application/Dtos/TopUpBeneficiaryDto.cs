namespace TopUp.Application;

public record TopUpBeneficiaryDto(Guid BeneficiaryId, Guid UserId, decimal Amount, decimal Fee);
