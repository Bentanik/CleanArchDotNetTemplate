namespace _Project_.Persistence.Repositories;

public class ExampleAggregateRepository
    : RepositoryBase<ExampleAggregate, Guid>, IExampleAggregateRepository
{
    public ExampleAggregateRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<ExampleAggregate?> FindByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Examples
            .AsTracking()
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
}