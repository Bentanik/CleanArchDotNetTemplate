namespace _Project_.Domain.Abstractions.Interfaces;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}

public interface IAggregateRoot<TId> : IAggregateRoot, IBaseEntity<TId> where TId : IEquatable<TId>
{
}
