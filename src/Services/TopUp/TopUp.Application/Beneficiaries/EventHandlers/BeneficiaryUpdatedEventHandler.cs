using MediatR;
using Microsoft.Extensions.Logging;
using TopUp.Domain;

namespace TopUp.Application;
public class BeneficiaryUpdatedEventHandler(ILogger<BeneficiaryUpdatedEventHandler> logger)
    : INotificationHandler<BeneficiaryUpdatedEvent>
{
    public Task Handle(BeneficiaryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
