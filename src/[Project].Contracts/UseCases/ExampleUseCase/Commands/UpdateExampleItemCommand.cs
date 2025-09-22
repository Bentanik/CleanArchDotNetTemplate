namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record DeleteExampleItemCommand(Guid ExampleId, Guid ExampleItemId)
    : ICommand;