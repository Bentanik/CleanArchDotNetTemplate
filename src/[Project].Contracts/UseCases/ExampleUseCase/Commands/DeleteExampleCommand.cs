namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record DeleteExampleCommand(Guid ExampleId)
    : ICommand;