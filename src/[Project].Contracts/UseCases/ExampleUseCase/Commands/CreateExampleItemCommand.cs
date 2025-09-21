namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record CreateExampleItemsCommand(Guid ExampleId, List<ExampleItemDto> ExampleItems)
    : ICommand;