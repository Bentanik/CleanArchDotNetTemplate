namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record UpdateExampleItemCommand(string RequestId, Guid ExampleId, Guid ExampleItemId, string ExampleItemText)
    : ICommand;