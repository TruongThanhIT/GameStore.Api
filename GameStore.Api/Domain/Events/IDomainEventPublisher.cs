using GameStore.Api.Domain.Events;

namespace GameStore.Api.Domain.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync(DomainEvent domainEvent);
}