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
        var item = new ExampleItemEntity(exampleText: exampleText, exampleId: this.Id);

        _items.Add(item);
    }

    public void Update(string? exampleText = null, ExampleValueObject? exampleValueObject = null, ExampleEnum? exampleStatus = null)
    {
        if (!string.IsNullOrWhiteSpace(exampleText))
            ExampleText = exampleText;

        if (exampleValueObject != null)
            ExampleValueObject = exampleValueObject;

        if (exampleStatus.HasValue)
            ExampleStatus = exampleStatus.Value;

        AddDomainEvent(ExampleUpdatedEvent.Of(Id.ToString()));
    }

    public void Delete()
    {
        AddDomainEvent(ExampleDeletedEvent.Of(Id.ToString()));
    }

    public void UpdateItem(Guid itemId, string exampleText)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId) ?? throw new InvalidOperationException("Item not found");
        item.UpdateExampleText(exampleText);

        AddDomainEvent(ExampleItemUpdatedEvent.Of(itemId.ToString()));
    }
}