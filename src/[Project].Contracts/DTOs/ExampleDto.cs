namespace _Project_.Contracts.DTOs;

public record ExampleDto(
    Guid? Id = null,
    string? ExampleText = null,
    DateTime? CreatedAt = null,
    List<ExampleItemDto>? Items = null
);