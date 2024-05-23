namespace TopUp.Application;

public record CreateBeneficiaryDto(
    Guid UserId,
    string BeneficiaryNickname,
    string Phone);
