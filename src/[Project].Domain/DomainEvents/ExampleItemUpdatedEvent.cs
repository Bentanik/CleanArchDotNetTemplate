namespace _Project_.Domain.DomainEvents;

public class ExampleItemUpdatedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleItemUpdatedEvent(string value)
    {
        Value = value;
    }

    public static ExampleItemUpdatedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ExampleText cannot be empty", nameof(value));

        return new ExampleItemUpdatedEvent(value);
    }
}