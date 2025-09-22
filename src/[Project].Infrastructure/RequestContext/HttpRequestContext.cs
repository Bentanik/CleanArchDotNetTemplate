using Microsoft.AspNetCore.Http;

namespace _Project_.Infrastructure.RequestContext;

public class HttpRequestContext : IRequestContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpRequestContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetIdempotencyKey()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return null;

        if (context.Request.Headers.TryGetValue("X-Request-Id", out var requestId))
        {
            return requestId.ToString();
        }

        return null;
    }
}