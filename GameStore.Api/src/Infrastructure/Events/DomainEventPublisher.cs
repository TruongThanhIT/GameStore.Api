using GameStore.Api.Domain.Events;

namespace GameStore.Api.Infrastructure.Events;

public class DomainEventPublisher(ILogger<DomainEventPublisher> logger) : IDomainEventPublisher
{
    public async Task PublishAsync(DomainEvent domainEvent)
    {
        // For now, just log the event. In a real application, this could send to a message bus, etc.
        logger.LogInformation("Domain event raised: {EventType} at {OccurredOn}",
            domainEvent.GetType().Name, domainEvent.OccurredOn);

        // Simulate async operation
        await Task.CompletedTask;
    }
}