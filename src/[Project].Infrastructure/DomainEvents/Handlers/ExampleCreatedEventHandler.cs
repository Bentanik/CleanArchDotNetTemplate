namespace _Project_.Infrastructure.DomainEvents.Handlers;

public class ExampleCreatedEventHandler : IDomainEventHandler<ExampleCreatedEvent>
{
    public Task Handle(ExampleCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example created: {domainEvent.ExampleText}");
        return Task.CompletedTask;
    }
}