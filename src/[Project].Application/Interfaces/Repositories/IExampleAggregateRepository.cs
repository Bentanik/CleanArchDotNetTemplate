namespace _Project_.Application.Interfaces.Repositories;

public interface IExampleAggregateRepository : IRepositoryBase<ExampleAggregate, Guid>
{
    Task<ExampleAggregate?> FindByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
}