namespace _Project_.Domain.DomainEvents;

public class ExampleItemDeletedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleItemDeletedEvent(string value)
    {
        Value = value;
    }

    public static ExampleItemDeletedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));

        return new ExampleItemDeletedEvent(value);
    }
}