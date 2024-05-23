namespace TopUp.Application;

public record UpdateBeneficiaryDto(
    Guid BeneficiaryId,
    string BeneficiaryNickname,
    string Phone);
