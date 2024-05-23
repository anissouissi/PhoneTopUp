using BuildingBlocks;

namespace TopUp.Application;

public record GetBeneficiariesByUserQuery(Guid UserId)
    : IQuery<GetBeneficiariesByUserResult>;

public record GetBeneficiariesByUserResult(IEnumerable<BeneficiaryDto> Beneficiaries);
