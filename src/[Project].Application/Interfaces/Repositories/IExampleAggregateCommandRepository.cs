namespace _Project_.Application.Interfaces.Repositories;

public interface IExampleAggregateCommandRepository : ICommandRepository<ExampleAggregate, Guid>
{
    Task<ExampleAggregate?> FindByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(ExampleAggregate? Aggregate, bool IsDuplicateText)> FindByIdAndCheckDuplicateExampleTextAsync(
    Guid id,
    string? exampleText = null,
    CancellationToken cancellationToken = default
    );
    Task<(ExampleAggregate? Aggregate, bool IsDuplicateExampleItemText)> FindAggregateWithItemAndCheckDuplicateAsync(
       Guid id,
       Guid exampleItemId,
       string? exampleText = null,
       CancellationToken cancellationToken = default
    );
}