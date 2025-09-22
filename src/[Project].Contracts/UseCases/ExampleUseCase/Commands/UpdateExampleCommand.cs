namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record UpdateExampleCommand(Guid ExampleId, string? ExampleText, string? ExampleValueObjectText, ExampleStatusEnumDto? ExampleStatus)
    : ICommand;