using MediatR;
using Microsoft.Extensions.Logging;
using TopUp.Domain;

namespace TopUp.Application;
public class BeneficiaryCreatedEventHandler(ILogger<BeneficiaryCreatedEventHandler> logger)
    : INotificationHandler<BeneficiaryCreatedEvent>
{
    public Task Handle(BeneficiaryCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
        return Task.CompletedTask;
    }
}
