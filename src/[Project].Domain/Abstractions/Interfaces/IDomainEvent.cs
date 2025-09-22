using MediatR;

namespace _Project_.Domain.Abstractions.Interfaces;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTimeOffset OccurredOn { get; }
}