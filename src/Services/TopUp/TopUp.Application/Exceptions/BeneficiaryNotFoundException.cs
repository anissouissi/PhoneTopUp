using BuildingBlocks;

namespace TopUp.Application;
public class BeneficiaryNotFoundException(Guid id) : NotFoundException("Beneficiary", id)
{
}
