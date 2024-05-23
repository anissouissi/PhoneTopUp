namespace BankAccount.Application;

public record CreateAccountDto(HolderDto Holder, decimal Balance);
