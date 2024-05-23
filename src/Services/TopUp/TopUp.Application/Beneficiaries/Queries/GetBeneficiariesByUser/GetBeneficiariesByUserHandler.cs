using Microsoft.EntityFrameworkCore;
using BuildingBlocks;
using TopUp.Domain;

namespace TopUp.Application;
public class GetBeneficiariesByUserHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetBeneficiariesByUserQuery, GetBeneficiariesByUserResult>
{
    public async Task<GetBeneficiariesByUserResult> Handle(GetBeneficiariesByUserQuery query, CancellationToken cancellationToken)
    {
        var beneficiaries = await dbContext.Beneficiaries
                        .Include(b => b.TopUps)
                        .AsNoTracking()
                        .Where(b => b.UserId == UserId.From(query.UserId))
                        .OrderBy(b => b.Nickname.Value)
                        .ToListAsync(cancellationToken);

        return new GetBeneficiariesByUserResult(beneficiaries.ToBeneficiaryDtoList());
    }
}
