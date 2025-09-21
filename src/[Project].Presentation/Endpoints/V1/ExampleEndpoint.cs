using _Project_.Contracts.UseCases.ExampleUseCase.Commands;
using _Project_.Presentation.Schemas.V1.Example.Requests;

namespace _Project_.Presentation.Endpoints.V1;

public static class ExampleEndpoint
{
    private const string EndpointName = "ExampleEndpoint";

    public static IVersionedEndpointRouteBuilder MapExampleEndpointApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup($"/api/v{{version:apiVersion}}/{EndpointName}")
            .HasApiVersion(1);

        group.MapPost(string.Empty, HandleCreateExampleAsync);
        group.MapPost("{exampleId}/example_items", HandleCreateExampleItemsAsync);

        return builder;
    }

    private static async Task<IResult> HandleCreateExampleAsync(
        ISender sender,
        [FromBody] CreateExampleRequest request)
    {
        var command = new CreateExampleCommand(
            ExampleText: request.ExampleText,
            ExampleValueObjectText: request.ExampleValueObjectText,
            ExampleStatus: request.ExampleStatus
        );
        var result = await sender.Send(command);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateExampleItemsAsync(
    ISender sender,
    [FromRoute] Guid exampleId,
    [FromBody] CreateExampleItemsRequest request)
    {
        var command = new CreateExampleItemsCommand(
            ExampleId: exampleId,
            ExampleItems: request.ExampleItems
        );

        var result = await sender.Send(command);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}