namespace BankAccount.Domain;

public record AccountCreatedEvent(Account Account) : IDomainEvent;
