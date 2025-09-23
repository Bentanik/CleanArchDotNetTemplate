namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record DeleteExampleCommand(string RequestId, Guid ExampleId)
    : ICommand, IIdempotentRequest;