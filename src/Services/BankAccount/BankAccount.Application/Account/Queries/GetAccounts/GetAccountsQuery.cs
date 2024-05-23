using BuildingBlocks;

namespace BankAccount.Application;

public record GetAccountsQuery()
    : IQuery<GetAccountsResult>;

public record GetAccountsResult(IEnumerable<AccountDto> Accounts);