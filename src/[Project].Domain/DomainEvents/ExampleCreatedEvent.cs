namespace _Project_.Domain.DomainEvents;

public class ExampleCreatedEvent : DomainEvent
{
    public string ExampleText { get; }

    private ExampleCreatedEvent(string exampleText)
    {
        ExampleText = exampleText;
    }

    public static ExampleCreatedEvent Of(string exampleText)
    {
        if (string.IsNullOrWhiteSpace(exampleText))
            throw new ArgumentException("ExampleText cannot be empty", nameof(exampleText));

        return new ExampleCreatedEvent(exampleText);
    }
}