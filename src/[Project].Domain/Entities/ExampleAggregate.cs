namespace _Project_.Domain.Entities;

public class ExampleAggregate : AggregateRoot<Guid>
{
    public string ExampleText { get; private set; } = default!;
    public ExampleValueObject ExampleValueObject { get; private set; } = default!;
    public ExampleStatusEnum ExampleStatus { get; private set; }

    private readonly List<ExampleItemEntity> _items = [];
    public IReadOnlyCollection<ExampleItemEntity> Items => _items.AsReadOnly();

    protected ExampleAggregate() { }

    private ExampleAggregate(Guid id, string exampleText, ExampleValueObject exampleValueObject, ExampleStatusEnum exampleStatus)
    {
        Id = id;
        ExampleText = exampleText ?? throw new ArgumentNullException(nameof(exampleText));
        ExampleValueObject = exampleValueObject ?? throw new ArgumentNullException(nameof(exampleValueObject));
        ExampleStatus = exampleStatus;

        AddDomainEvent(ExampleCreatedEvent.Of(exampleText));
    }

    public static ExampleAggregate Create(string exampleText, ExampleValueObject exampleValueObject, ExampleStatusEnum exampleStatus, Guid? id = null)
    {
        var aggregateId = id ?? Guid.NewGuid();
        return new ExampleAggregate(aggregateId, exampleText, exampleValueObject, exampleStatus);
    }

    public void AddItem(string exampleText)
    {
        var item = new ExampleItemEntity(exampleText: exampleText, exampleId: this.Id);
        _items.Add(item);

        AddDomainEvent(ExampleItemUpdatedEvent.Of(Id.ToString()));
    }

    public void Update(string? exampleText = null, ExampleValueObject? exampleValueObject = null, ExampleStatusEnum? exampleStatus = null)
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
    public void DeleteItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId) ?? throw new InvalidOperationException("Item not found");

        AddDomainEvent(ExampleItemDeletedEvent.Of(itemId.ToString()));
    }
}