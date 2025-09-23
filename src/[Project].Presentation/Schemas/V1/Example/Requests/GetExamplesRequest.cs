namespace _Project_.Presentation.Schemas.V1.Example.Requests;

public record GetExamplesRequest(
    Guid? ExampleId = null,
    string? ExampleText = null,
    GetExamplesExpandEnumDto Expand = GetExamplesExpandEnumDto.None,
    ExampleSortFieldEnumDto SortBy = ExampleSortFieldEnumDto.CreatedAt,
    SortDirectionEnumDto SortDir = SortDirectionEnumDto.Asc,
    int Page = 1,
    int PageSize = 10);
