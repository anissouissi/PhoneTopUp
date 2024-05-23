using BuildingBlocks;

namespace TopUp.Application;

public record GetBeneficiariesQuery : IQuery<GetBeneficiariesResult>;

public record GetBeneficiariesResult(IEnumerable<BeneficiaryDto> Beneficiaries);