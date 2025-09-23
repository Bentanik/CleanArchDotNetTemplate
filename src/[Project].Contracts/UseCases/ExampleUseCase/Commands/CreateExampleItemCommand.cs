namespace _Project_.Contracts.UseCases.ExampleUseCase.Commands;

public record CreateExampleItemsCommand(string RequestId, Guid ExampleId, List<ExampleItemDto> ExampleItems)
    : ICommand, IIdempotentRequest;