namespace _Project_.Domain.Abstractions.Interfaces;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTimeOffset OccurredOn { get; }
}