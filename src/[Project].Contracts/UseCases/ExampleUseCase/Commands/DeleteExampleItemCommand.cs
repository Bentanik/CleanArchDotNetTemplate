namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record UpdateExampleItemCommand(Guid ExampleId, Guid ExampleItemId, string ExampleItemText)
    : ICommand;