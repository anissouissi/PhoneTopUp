using MediatR;
using Microsoft.Extensions.Logging;
using BankAccount.Domain;

namespace BankAccount.Application;
public class AccountCreatedEventHandler(ILogger<AccountCreatedEventHandler> logger)
    : INotificationHandler<AccountCreatedEvent>
{
    public Task Handle(AccountCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}
