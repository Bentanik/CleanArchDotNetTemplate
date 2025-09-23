namespace _Project_.Application.Interfaces.Repositories;

public interface IQueryRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
    Task<IEnumerable<TEntity>> FindAsync(
        Func<TEntity, bool> predicate,
        CancellationToken cancellationToken = default
    );
}