namespace _Project_.Domain.DomainEvents;

public class ExampleUpdatedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleUpdatedEvent(string value)
    {
        Value = value;
    }

    public static ExampleUpdatedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ExampleText cannot be empty", nameof(value));

        return new ExampleUpdatedEvent(value);
    }
}