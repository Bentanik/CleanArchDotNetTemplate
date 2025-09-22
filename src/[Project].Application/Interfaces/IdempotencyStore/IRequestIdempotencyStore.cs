namespace _Project_.Application.Interfaces.IdempotencyStore;

public interface IRequestIdempotencyStore
{
    Task<bool> ExistsAsync(string requestId, CancellationToken cancellationToken = default);

    Task SaveAsync(string requestId, CancellationToken cancellationToken = default);
}