namespace _Project_.Infrastructure.DomainEvents;

public record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent) : INotification
    where TDomainEvent : IDomainEvent;