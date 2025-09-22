namespace _Project_.Infrastructure.DomainEvents.Handlers;

public class ExampleEventHandler : IDomainEventHandler<ExampleCreatedEvent>
{
    public Task Handle(ExampleCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example created: {domainEvent.Value}");
        return Task.CompletedTask;
    }
}