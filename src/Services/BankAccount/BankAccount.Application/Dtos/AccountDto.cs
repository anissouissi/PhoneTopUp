namespace BankAccount.Application;

public record AccountDto(
    Guid Id,
    Guid HolderId,
    Guid AccountNumber,
    decimal Balance);
