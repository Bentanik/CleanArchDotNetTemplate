namespace _Project_.Domain.Entities;

public class ExampleItemEntity : BaseEntity<Guid>
{
    public string ExampleText { get; private set; } = default!;

    // FK
    public Guid ExampleId { get; private set; }

    // Navigation
    public ExampleAggregate Example { get; private set; } = default!;

    protected ExampleItemEntity() { }

    internal ExampleItemEntity(string exampleText, Guid exampleId, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        ExampleText = exampleText ?? throw new ArgumentNullException(nameof(exampleText));
        ExampleId = exampleId;
    }

    public void UpdateExampleText(string exampleText)
    {
        ExampleText = exampleText ?? throw new ArgumentNullException(nameof(exampleText));
    }
}