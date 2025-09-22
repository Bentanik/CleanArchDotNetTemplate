namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record CreateExampleCommand(string RequestId, string ExampleText, string ExampleValueObjectText, ExampleStatusEnumDto ExampleStatus)
    : ICommand, IIdempotentRequest;