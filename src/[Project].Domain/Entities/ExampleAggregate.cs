namespace _Project_.Domain.Entities;

public class ExampleAggregate : AggregateRoot<Guid>
{
    public string ExampleText { get; private set; } = default!;
    public ExampleValueObject ExampleValueObject { get; private set; } = default!;
    public ExampleEnum ExampleStatus { get; private set; }

    private readonly List<ExampleItemEntity> _items = [];
    public IReadOnlyCollection<ExampleItemEntity> Items => _items.AsReadOnly();

    protected ExampleAggregate() { }

    private ExampleAggregate(Guid id, string exampleText, ExampleValueObject exampleValueObject, ExampleEnum exampleStatus)
    {
        Id = id;
        ExampleText = exampleText ?? throw new ArgumentNullException(nameof(exampleText));
        ExampleValueObject = exampleValueObject ?? throw new ArgumentNullException(nameof(exampleValueObject));
        ExampleStatus = exampleStatus;

        AddDomainEvent(ExampleCreatedEvent.Of(exampleText));
    }

    public static ExampleAggregate Create(string exampleText, ExampleValueObject exampleValueObject, ExampleEnum exampleStatus, Guid? id = null)
    {
        var aggregateId = id ?? Guid.NewGuid();
        return new ExampleAggregate(aggregateId, exampleText, exampleValueObject, exampleStatus);
    }

    public void AddItem(string exampleText)
    {
        var item = new ExampleItemEntity(exampleText);
        _items.Add(item);
    }
}