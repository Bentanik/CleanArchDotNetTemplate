namespace _Project_.Domain.ValueObjects;

public class ExampleValueObject : ValueObject
{
    public string ExampleValue { get; }

    private ExampleValueObject(string exampleValue)
    {
        ExampleValue = exampleValue;
    }

    public static ExampleValueObject Of(string exampleValue)
    {
        return new ExampleValueObject(exampleValue);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ExampleValue;
    }
}