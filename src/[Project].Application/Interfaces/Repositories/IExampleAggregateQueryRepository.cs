namespace _Project_.Application.Interfaces.Repositories;

public interface IExampleAggregateQueryRepository
    : IQueryRepository<ExampleAggregate>
{
    Task<PageDto<ExampleAggregate>> GetExampleAggregatesAsync(
        Guid? exampleId = null,
        string? exampleText = null,
        ExampleSortFieldEnumDto sortBy = ExampleSortFieldEnumDto.CreatedAt,
        SortDirectionEnumDto sortDir = SortDirectionEnumDto.Asc,
        GetExamplesExpandEnumDto expand = GetExamplesExpandEnumDto.None,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}
