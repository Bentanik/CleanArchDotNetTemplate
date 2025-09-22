namespace _Project_.Infrastructure.RequestContext;

public class CompositeRequestContext : IRequestContext
{
    private readonly HttpRequestContext? _http;

    public CompositeRequestContext(
        HttpRequestContext? http)
    {
        _http = http;
    }

    public string? GetIdempotencyKey()
        => _http?.GetIdempotencyKey();
}