namespace _Project_.Infrastructure.DomainEvents.Handlers;

public class ExampleEventHandler 
    : IDomainEventHandler<ExampleCreatedEvent>,
     IDomainEventHandler<ExampleUpdatedEvent>,
     IDomainEventHandler<ExampleDeletedEvent>,
     IDomainEventHandler<ExampleItemUpdatedEvent>,
     IDomainEventHandler<ExampleItemDeletedEvent>
{
    public async Task Handle(ExampleCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example created: {domainEvent.Value}");
        await Task.CompletedTask;
    }

    public async Task Handle(ExampleUpdatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example updated: {domainEvent.Value}");
        await Task.CompletedTask;
    }

    public async Task Handle(ExampleDeletedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example deleted: {domainEvent.Value}");
        await Task.CompletedTask;
    }

    public async Task Handle(ExampleItemUpdatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example item updated: {domainEvent.Value}");
        await Task.CompletedTask;
    }

    public async Task Handle(ExampleItemDeletedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Example item deleted: {domainEvent.Value}");
        await Task.CompletedTask;
    }
}