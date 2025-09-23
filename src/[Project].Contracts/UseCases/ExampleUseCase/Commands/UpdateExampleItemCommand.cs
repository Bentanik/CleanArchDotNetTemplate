namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record DeleteExampleItemCommand(string RequestId, Guid ExampleId, Guid ExampleItemId)
    : ICommand;