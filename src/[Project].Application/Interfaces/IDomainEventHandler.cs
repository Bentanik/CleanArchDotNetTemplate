namespace _Project_.Application.Interfaces;

public interface IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}