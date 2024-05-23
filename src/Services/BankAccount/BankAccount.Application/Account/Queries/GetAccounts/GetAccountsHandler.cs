using Mapster;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks;

namespace BankAccount.Application;
public class GetAccountsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountsQuery, GetAccountsResult>
{
    public async Task<GetAccountsResult> Handle(GetAccountsQuery query, CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Accounts.ToListAsync(cancellationToken);

        var result = accounts.ToAccountDtoList();

        return new GetAccountsResult(result);
    }
}
