namespace _Project_.Domain.DomainEvents;

public class ExampleItemsCreatedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleItemsCreatedEvent(string value)
    {
        Value = value;
    }

    public static ExampleItemsCreatedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));

        return new ExampleItemsCreatedEvent(value);
    }
}