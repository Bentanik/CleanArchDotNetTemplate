namespace _Project_.Persistence.Repositories;

public class ExampleAggregateQueryRepository
    : QueryRepository<ExampleAggregate>, IExampleAggregateQueryRepository
{
    public ExampleAggregateQueryRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<PageDto<ExampleAggregate>> GetExampleAggregatesAsync(
       Guid? exampleId = null,
       string? exampleText = null,
       ExampleSortFieldEnumDto sortBy = ExampleSortFieldEnumDto.CreatedAt,
       SortDirectionEnumDto sortDir = SortDirectionEnumDto.Asc,
       GetExamplesExpandEnumDto expand = GetExamplesExpandEnumDto.None,
       int page = 1,
       int pageSize = 10,
       CancellationToken cancellationToken = default)
    {
        IQueryable<ExampleAggregate> query = _dbSet.AsNoTracking();

        // Filter
        if (exampleId.HasValue)
            query = query.Where(e => e.Id == exampleId.Value);

        if (!string.IsNullOrWhiteSpace(exampleText))
            query = query.Where(e => e.ExampleText.Contains(exampleText));

        // Sort
        query = sortBy switch
        {
            ExampleSortFieldEnumDto.CreatedAt =>
                sortDir == SortDirectionEnumDto.Asc
                    ? query.OrderBy(e => e.CreatedAt)
                    : query.OrderByDescending(e => e.CreatedAt),
            _ => query
        };

        // Expand
        if (expand.HasFlag(GetExamplesExpandEnumDto.ExampleItems))
            query = query.Include(e => e.Items);

        // Total count
        var total = await query.CountAsync(cancellationToken);

        // Pagination
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PageDto<ExampleAggregate>(
            Page: page,
            PageSize: pageSize,
            Total: total,
            Items: items
        );
    }
}