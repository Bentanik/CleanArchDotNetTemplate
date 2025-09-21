namespace _Project_.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e,
                "Unhandled exception occurred. Path: {Path}, Message: {Message}, TraceId: {TraceId}",
                context.Request.Path, e.Message, context.TraceIdentifier);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = GetStatusCode(exception);

        var errors = GetErrors(exception);

        var response = new
        {
            isSuccess = false,
            isFailure = true,
            errors
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            AuthorizeException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static IEnumerable<object> GetErrors(Exception exception)
    {
        if (exception is FluentValidation.ValidationException validationEx)
            return validationEx.Errors.Select(e => new { code = "VALIDATION_ERROR", message = e.ErrorMessage });

        if (exception is DomainException domainEx)
            return [new { code = domainEx.ErrorCode, message = domainEx.Message }];

        if (exception is BadRequestException badReq)
            return [new { code = "BAD_REQUEST", message = badReq.Message }];

        return [new { code = "INTERNAL_ERROR", message = exception.Message }];
    }
}