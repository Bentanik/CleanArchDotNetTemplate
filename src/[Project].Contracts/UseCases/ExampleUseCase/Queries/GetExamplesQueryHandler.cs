namespace _Project_.Contracts.UseCases.ExampleUseCase.Queries;

public record GetExamplesQuery(
    Guid? ExampleId = null,
    string? ExampleText = null,
    ExampleSortFieldEnumDto SortBy = ExampleSortFieldEnumDto.CreatedAt,
    SortDirectionEnumDto SortDir = SortDirectionEnumDto.Asc,
    GetExamplesExpandEnumDto Expand = GetExamplesExpandEnumDto.None,
    int Page = 1,
    int PageSize = 10)
    : IQuery<PageDto<ExampleDto>>;
