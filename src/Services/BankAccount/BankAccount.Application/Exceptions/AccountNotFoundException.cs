using BuildingBlocks;

namespace BankAccount.Application;

public class AccountNotFoundException : NotFoundException
{
    public AccountNotFoundException(Guid id) : base("Account", id)
    {

    }

    public AccountNotFoundException(string accountNumber) : base("Account", accountNumber)
    {

    }
}
