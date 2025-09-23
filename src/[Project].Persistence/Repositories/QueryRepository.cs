namespace _Project_.Persistence.Repositories;

public class QueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public QueryRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_dbSet.AsNoTracking().Where(predicate).ToList());
    }
}