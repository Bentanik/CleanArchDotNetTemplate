namespace _Project_.Contracts.Messages;

public enum ExampleMessages
{
    [Message("Created successfully", "EXAMPLE_01")]
    CreatedSuccessfully,

    [Message("Duplicate example text", "EXAMPLE_02")]
    DuplicateExampleText
}