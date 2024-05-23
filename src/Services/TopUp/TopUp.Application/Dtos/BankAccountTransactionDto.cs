namespace TopUp.Application;

public record BankAccountTransactionDto(Guid AccountNumber, decimal Amount);

public record DebitAccountRequest(BankAccountTransactionDto Transaction);