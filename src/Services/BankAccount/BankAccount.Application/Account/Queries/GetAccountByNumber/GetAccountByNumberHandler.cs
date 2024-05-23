using Mapster;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks;
using BankAccount.Domain;

namespace BankAccount.Application;
public class GetAccountByNumberHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetAccountByNumberQuery, GetAccountByNumberResult>
{
    public async Task<GetAccountByNumberResult> Handle(GetAccountByNumberQuery query, CancellationToken cancellationToken)
    {
        var account = await dbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AccountNumber == AccountNumber.From(query.AccountNumber), cancellationToken);

        if (account is null)
        {
            throw new AccountNotFoundException(query.AccountNumber);
        }

        var result = account.ToAccountDto();

        return new GetAccountByNumberResult(result);
    }
}
