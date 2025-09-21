namespace _Project_.Persistence.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected readonly AppDbContext _context;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<IEnumerable<TEntity>> FindAllAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool isTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (!isTracking)
            query = query.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> FindSingleAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool isTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (!isTracking)
            query = query.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool isTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (!isTracking)
            query = query.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveMultipleAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        return Task.CompletedTask;
    }
}