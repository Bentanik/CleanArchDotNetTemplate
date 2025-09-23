namespace _Project_.Contracts.DTOs;

public record ExampleDto(
    Guid Id,
    string ExampleText,
    DateTime CreatedAt,
    List<ExampleItemDto>? Items = null
);