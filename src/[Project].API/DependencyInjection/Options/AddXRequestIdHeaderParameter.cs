using Microsoft.OpenApi.Models;

namespace _Project_.API.DependencyInjection.Options;

public class AddXRequestIdHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = [];

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Request-Id",
            In = ParameterLocation.Header,
            Description = "Idempotency key for request",
            Required = false
        });
    }
}