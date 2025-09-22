namespace _Project_.Application.Behaviors;

public class IdempotencyBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IIdempotentRequest
{
    private readonly IRequestIdempotencyStore _store;

    public IdempotencyBehavior(IRequestIdempotencyStore store)
    {
        _store = store;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestId = request.RequestId;

        if (string.IsNullOrWhiteSpace(requestId))
        {
            throw new ArgumentException("RequestId must be provided for idempotent requests.");
        }

        if (await _store.ExistsAsync(requestId, cancellationToken))
        {
            throw new InvalidOperationException($"Duplicate request detected: {requestId}");
        }

        var response = await next();

        await _store.SaveAsync(requestId, cancellationToken);

        return response;
    }
}