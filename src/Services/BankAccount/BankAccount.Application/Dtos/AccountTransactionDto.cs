namespace BankAccount.Application;

public record AccountTransactionDto(Guid AccountNumber, decimal Amount);
