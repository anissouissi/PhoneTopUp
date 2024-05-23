using MediatR;
using Microsoft.Extensions.Logging;
using BankAccount.Domain;

namespace BankAccount.Application;
public class AccountUpdatedEventHandler(ILogger<AccountUpdatedEventHandler> logger)
    : INotificationHandler<AccountUpdatedEvent>
{
    public Task Handle(AccountUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
