namespace TopUp.Application;

public record BeneficiaryDto(
    Guid Id,
    Guid UserId,
    string BeneficiaryNickname,
    string Phone,
    List<TopUpDto> TopUps);
