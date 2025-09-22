using _Project_.Domain.DomainEvents;

namespace _Project_.Application.Interfaces.DomainEvents;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(ExampleUpdatedEvent domainEvent, CancellationToken cancellationToken = default);
    Task Handle(ExampleDeletedEvent domainEvent, CancellationToken cancellationToken = default);
    Task Handle(ExampleItemUpdatedEvent domainEvent, CancellationToken cancellationToken = default);
}