namespace _Project_.Domain.Abstractions.Interfaces;

public interface IAggregateRoot<TId> : IBaseEntity<TId> where TId : IEquatable<TId>
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
