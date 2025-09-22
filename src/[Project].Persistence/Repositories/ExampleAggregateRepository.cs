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

    public async Task<(ExampleAggregate? Aggregate, bool IsDuplicateText)> FindByIdAndCheckDuplicateExampleTextAsync(
    Guid id, string? exampleText = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<ExampleAggregate>();

        var result = await query
            .Where(x => x.Id == id)
            .Select(x => new
            {
                Aggregate = x,
                IsDuplicateText = exampleText != null && query.Any(y => y.Id != id && y.ExampleText == exampleText)
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null)
            return (null, false);

        return (result.Aggregate, result.IsDuplicateText);
    }

    public async Task<(ExampleAggregate? Aggregate, bool IsDuplicateExampleItemText)>
    FindAggregateWithItemAndCheckDuplicateAsync(
        Guid id,
        Guid exampleItemId,
        string? exampleText = null,
        CancellationToken cancellationToken = default)
    {
        var aggregate = await _context.Set<ExampleAggregate>()
            .Where(x => x.Id == id)
            .Select(x => new
            {
                Aggregate = x,
                ExampleItem = x.Items.FirstOrDefault(i => i.Id == exampleItemId),
                IsDuplicateExampleItemText = exampleText != null &&
                                             x.Items.Any(i => i.ExampleText == exampleText)
            })
            .FirstOrDefaultAsync(cancellationToken);
            
        if (aggregate == null)
            return (null, false);

        return (aggregate.Aggregate, aggregate.IsDuplicateExampleItemText);
    }
}