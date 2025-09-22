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
        group.MapPut("{exampleId}", HandleUpdateExampleAsync);
        group.MapDelete("{exampleId}", HandleDeleteExampleAsync);

        group.MapPost("{exampleId}/example_items", HandleCreateExampleItemsAsync);
        group.MapPut("{exampleId}/example_item/{exampleItemId}", HandleUpdateExampleItemAsync);
        group.MapDelete("{exampleId}/example_item/{exampleItemId}", HandleDeleteExampleItemAsync);

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

    private static async Task<IResult> HandleUpdateExampleAsync(
       ISender sender,
       [FromRoute] Guid exampleId,
       [FromBody] UpdateExampleRequest request)
    {
        var command = new UpdateExampleCommand(
            ExampleId: exampleId,
            ExampleText: request.ExampleText,
            ExampleValueObjectText: request.ExampleValueObjectText,
            ExampleStatus: request.ExampleStatus
        );

        var result = await sender.Send(command);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleDeleteExampleAsync(
        ISender sender,
        [FromRoute] Guid exampleId)
    {
        var command = new DeleteExampleCommand(
            ExampleId: exampleId
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

    private static async Task<IResult> HandleUpdateExampleItemAsync(
        ISender sender,
        [FromRoute] Guid exampleId,
        [FromRoute] Guid exampleItemId,
        [FromBody] UpdateExampleItemRequest request)
    {
        var command = new UpdateExampleItemCommand(
            ExampleId: exampleId,
            ExampleItemId: exampleItemId,
            ExampleItemText: request.ExampleItemText
        );

        var result = await sender.Send(command);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleDeleteExampleItemAsync(
        ISender sender,
        [FromRoute] Guid exampleId,
        [FromRoute] Guid exampleItemId)
    {
        var command = new DeleteExampleItemCommand(
            ExampleId: exampleId,
            ExampleItemId: exampleItemId
        );

        var result = await sender.Send(command);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }
}