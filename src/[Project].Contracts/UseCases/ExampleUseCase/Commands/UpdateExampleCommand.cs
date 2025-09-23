namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record UpdateExampleCommand(string RequestId, Guid ExampleId, string? ExampleText, string? ExampleValueObjectText, ExampleStatusEnumDto? ExampleStatus)
    : ICommand, IIdempotentRequest;