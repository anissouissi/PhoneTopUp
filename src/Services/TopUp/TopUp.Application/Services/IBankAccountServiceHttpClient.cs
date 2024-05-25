namespace TopUp.Application;

public interface IBankAccountServiceHttpClient
{
    Task DebitAccount(BankAccountTransactionDto transaction);
}
