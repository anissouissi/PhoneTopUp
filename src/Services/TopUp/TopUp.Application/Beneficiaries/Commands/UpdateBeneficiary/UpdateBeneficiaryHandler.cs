using BuildingBlocks;
using TopUp.Domain;

namespace TopUp.Application;
public class UpdateBeneficiaryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateBeneficiaryCommand, UpdateBeneficiaryResult>
{
    public async Task<UpdateBeneficiaryResult> Handle(UpdateBeneficiaryCommand command, CancellationToken cancellationToken)
    {
        var beneficiaryId = BeneficiaryId.From(command.Beneficiary.BeneficiaryId);
        var beneficiary = await dbContext.Beneficiaries
            .FindAsync([beneficiaryId], cancellationToken: cancellationToken);

        if (beneficiary is null)
        {
            throw new BeneficiaryNotFoundException(command.Beneficiary.BeneficiaryId);
        }

        UpdateBeneficiaryWithNewValues(beneficiary, command.Beneficiary);

        dbContext.Beneficiaries.Update(beneficiary);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateBeneficiaryResult(true);
    }

    public static void UpdateBeneficiaryWithNewValues(Beneficiary beneficiary, UpdateBeneficiaryDto beneficiaryDto)
    {
        beneficiary.Update(
            nickname: BeneficiaryNickname.From(beneficiaryDto.BeneficiaryNickname),
            phone: Phone.From(beneficiaryDto.Phone)
            );
    }
}
