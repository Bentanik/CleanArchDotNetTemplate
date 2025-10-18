using _Project_.Contracts.UseCases.ExampleUseCase.Commands;
using _Project_.Contracts.UseCases.ExampleUseCase.Queries;
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

        group.MapGet(string.Empty, HandleGetExamplesAsync);
        
        return builder;
    }

    private static async Task<IResult> HandleCreateExampleAsync(
        IMessageBus messageBus,
        IRequestContext requestContext,
        [FromBody] CreateExampleRequest request,
        CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey() 
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var command = new CreateExampleCommand(
            RequestId: requestId,
            ExampleText: request.ExampleText,
            ExampleValueObjectText: request.ExampleValueObjectText,
            ExampleStatus: request.ExampleStatus
        );
        var result = await messageBus.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdateExampleAsync(
       IMessageBus messageBus,
       IRequestContext requestContext,
       [FromRoute] Guid exampleId,
       [FromBody] UpdateExampleRequest request,
       CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var command = new UpdateExampleCommand(
            RequestId: requestId,
            ExampleId: exampleId,
            ExampleText: request.ExampleText,
            ExampleValueObjectText: request.ExampleValueObjectText,
            ExampleStatus: request.ExampleStatus
        );

        var result = await messageBus.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDeleteExampleAsync(
        IMessageBus messageBus,
        IRequestContext requestContext,
        [FromRoute] Guid exampleId,
        CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var command = new DeleteExampleCommand(
            RequestId: requestId,
            ExampleId: exampleId
        );

        var result = await messageBus.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleCreateExampleItemsAsync(
        IMessageBus messageBus,
        IRequestContext requestContext,
        [FromRoute] Guid exampleId,
        [FromBody] CreateExampleItemsRequest request,
        CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var exampleItemDtos = request.ExampleItems
             .Select(exampleItem => new ExampleItemDto(
                 ExampleText: exampleItem.ExampleText
             ))
             .ToList();

        var command = new CreateExampleItemsCommand(
            RequestId: requestId,
            ExampleId: exampleId,
            ExampleItems: exampleItemDtos
        );

        var result = await messageBus.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleUpdateExampleItemAsync(
        IMessageBus messageBus,
        IRequestContext requestContext,
        [FromRoute] Guid exampleId,
        [FromRoute] Guid exampleItemId,
        [FromBody] UpdateExampleItemRequest request,
        CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var command = new UpdateExampleItemCommand(
            RequestId: requestId,
            ExampleId: exampleId,
            ExampleItemId: exampleItemId,
            ExampleItemText: request.ExampleItemText
        );

        var result = await messageBus.Send(command, ct);
        return Results.Ok(result);
    }

    private static async Task<IResult> HandleDeleteExampleItemAsync(
        IMessageBus messageBus,
        IRequestContext requestContext,
        [FromRoute] Guid exampleId,
        [FromRoute] Guid exampleItemId,
        CancellationToken ct)
    {
        string requestId = requestContext.GetIdempotencyKey()
            ?? throw new ArgumentException("X-Request-Id header must be provided for idempotent requests.");

        var command = new DeleteExampleItemCommand(
            RequestId: requestId,
            ExampleId: exampleId,
            ExampleItemId: exampleItemId
        );

        var result = await messageBus.Send(command, ct);
        return result.IsFailure ? Results.BadRequest(result) : Results.Ok(result);
    }

    private static async Task<IResult> HandleGetExamplesAsync(
        [FromServices] IMessageBus messageBus,
        [AsParameters] GetExamplesRequest request,
        CancellationToken ct
    )
    {
        var query = new GetExamplesQuery(
            ExampleId: request.ExampleId,
            ExampleText: request.ExampleText,
            SortBy: request.SortBy,
            SortDir: request.SortDir,
            Expand: request.Expand,
            Page: request.Page,
            PageSize: request.PageSize
        );

        var result = await messageBus.Send(query, ct);
        return Results.Ok(result);
    }
}