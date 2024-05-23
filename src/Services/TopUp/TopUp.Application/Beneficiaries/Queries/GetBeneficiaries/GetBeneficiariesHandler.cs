using Microsoft.EntityFrameworkCore;
using BuildingBlocks;

namespace TopUp.Application;
public class GetBeneficiariesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetBeneficiariesQuery, GetBeneficiariesResult>
{
    public async Task<GetBeneficiariesResult> Handle(GetBeneficiariesQuery query, CancellationToken cancellationToken)
    {
        var beneficiaries = await dbContext.Beneficiaries
                       .Include(b => b.TopUps)
                       .OrderBy(b => b.Nickname.Value)
                       .ToListAsync(cancellationToken);

        return new GetBeneficiariesResult(beneficiaries.ToBeneficiaryDtoList());
    }
}
