using _Project_.Contracts.UseCases.ExampleUseCase.Queries;

namespace _Project_.Application.UseCases.V1.Example.Queries;

public sealed class GetExamplesQueryHandler : IQueryHandler<GetExamplesQuery, PageDto<ExampleDto>>
{
    private readonly IExampleAggregateQueryRepository _exampleAggregateRepo;

    public GetExamplesQueryHandler(IExampleAggregateQueryRepository exampleAggregateRepo)
    {
        _exampleAggregateRepo = exampleAggregateRepo;
    }

    public async Task<Result<PageDto<ExampleDto>>> Handle(GetExamplesQuery query, CancellationToken cancellationToken)
    {
        var pagedEntities = await _exampleAggregateRepo.GetExampleAggregatesAsync(
            exampleId: query.ExampleId,
            exampleText: query.ExampleText,
            sortBy: query.SortBy,
            sortDir: query.SortDir,
            expand: query.Expand,
            page: query.Page,
            pageSize: query.PageSize,
            cancellationToken: cancellationToken
        );

        var dto = new PageDto<ExampleDto>(
            Page: pagedEntities.Page,
            PageSize: pagedEntities.PageSize,
            Total: pagedEntities.Total,
            Items: pagedEntities.Items.Select(e => new ExampleDto(
                e.Id,
                e.ExampleText,
                (DateTime)e.CreatedAt!,
                query.Expand.HasFlag(GetExamplesExpandEnumDto.ExampleItems)
                    ? e.Items.Select(i => new ExampleItemDto(i.Id, i.ExampleText)).ToList()
                    : null
            )).ToList()
        );

        return Result.Success(
            data: dto,
            code: "200",
            message: "Success"
        );
    }
}