using BuildingBlocks;
using TopUp.Domain;

namespace TopUp.Application;
public class DeleteBeneficiaryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteBeneficiaryCommand, DeleteBeneficiaryResult>
{
    public async Task<DeleteBeneficiaryResult> Handle(DeleteBeneficiaryCommand command, CancellationToken cancellationToken)
    {
        var beneficiaryId = BeneficiaryId.From(command.BeneficiaryId);
        var beneficiary = await dbContext.Beneficiaries
            .FindAsync([beneficiaryId], cancellationToken: cancellationToken);

        if (beneficiary is null)
        {
            throw new BeneficiaryNotFoundException(command.BeneficiaryId);
        }

        dbContext.Beneficiaries.Remove(beneficiary);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBeneficiaryResult(true);
    }
}
