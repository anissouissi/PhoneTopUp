namespace BankAccount.Domain;

public record AccountUpdatedEvent(Account Account) : IDomainEvent;
