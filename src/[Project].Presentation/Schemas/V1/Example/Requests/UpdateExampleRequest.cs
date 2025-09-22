namespace _Project_.Presentation.Schemas.V1.Example.Requests;

public record UpdateExampleRequest(string? ExampleText, string? ExampleValueObjectText, ExampleEnumDto? ExampleStatus);