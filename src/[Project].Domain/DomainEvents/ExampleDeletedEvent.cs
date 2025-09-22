namespace _Project_.Domain.DomainEvents;

public class ExampleDeletedEvent : DomainEvent
{
    public string Value { get; }

    private ExampleDeletedEvent(string value)
    {
        Value = value;
    }

    public static ExampleDeletedEvent Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ExampleText cannot be empty", nameof(value));

        return new ExampleDeletedEvent(value);
    }
}