using Microsoft.Extensions.Caching.Memory;

namespace _Project_.Infrastructure.IdempotencyStore;

public class InMemoryIdempotencyStore : IRequestIdempotencyStore
{
    private readonly IMemoryCache _cache;

    public InMemoryIdempotencyStore(IMemoryCache cache) => _cache = cache;

    public Task<bool> ExistsAsync(string requestId, CancellationToken cancellationToken = default)
        => Task.FromResult(_cache.TryGetValue(requestId, out _));

    public Task SaveAsync(string requestId, CancellationToken cancellationToken = default)
    {
        _cache.Set(requestId, true, TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }
}