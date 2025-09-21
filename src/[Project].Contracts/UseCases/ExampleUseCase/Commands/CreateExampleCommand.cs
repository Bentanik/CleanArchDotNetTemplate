namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record CreateExampleCommand(string ExampleText, string ExampleValueObjectText, ExampleEnumDto ExampleStatus)
    : ICommand;