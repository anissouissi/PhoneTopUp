using BuildingBlocks;

namespace BankAccount.Application;

public record GetAccountByNumberQuery(Guid AccountNumber)
    : IQuery<GetAccountByNumberResult>;

public record GetAccountByNumberResult(AccountDto Account);