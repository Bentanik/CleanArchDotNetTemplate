using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Presentation.Endpoints.V1;

public static class ExampleEndpoint
{
    public const string EndpointName = "ExampleEndpoint";

    public static IVersionedEndpointRouteBuilder MapExampleEndpointApiV1(this IVersionedEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup($"/api/v{{version:apiVersion}}/{EndpointName}")
            .HasApiVersion(1);

        group.MapPost(string.Empty, HandleCreateExampleAsync);

        return builder;
    }
    private static async Task<IResult> HandleCreateExampleAsync(ISender sender,
        [FromBody] CreateExampleCommand request
    )
    {
        var result = await sender.Send(request);
        if (result.IsFailure == true)
            return Results.BadRequest(result);

        return Results.Ok(result);
    }
}