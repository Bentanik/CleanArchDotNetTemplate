namespace _Project_.Domain.Entities;

public class ExampleItemEntity : BaseEntity<Guid>
{
    public string ExampleText { get; private set; } = default!;

    protected ExampleItemEntity() { }

    internal ExampleItemEntity(string exampleText, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        ExampleText = exampleText ?? throw new ArgumentNullException(nameof(exampleText));
    }
}