namespace _Project_.Contracts.DTOs;

public record PageDto<T>(
    int Page,
    int PageSize,
    int Total,
    IEnumerable<T> Items
);