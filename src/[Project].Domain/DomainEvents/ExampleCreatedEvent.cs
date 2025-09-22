namespace _Project_.Domain.DomainEvents;

public class ExampleCreatedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleCreatedEvent(string value)
    {
        Value = value;
    }

    public static ExampleCreatedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));

        return new ExampleCreatedEvent(value);
    }
}