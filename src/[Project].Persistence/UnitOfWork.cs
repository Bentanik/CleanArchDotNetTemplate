namespace _Project_.Persistence;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    private readonly IDomainEventDispatcher _dispatcher;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(AppDbContext dbContext, IDomainEventDispatcher dispatcher)
    {
        _dbContext = dbContext;
        _dispatcher = dispatcher;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null) return;
        _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) return;

        try
        {
            await SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) return;

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Update audit fields
        var date = DateTime.UtcNow;
        foreach (var entry in _dbContext.ChangeTracker.Entries<IBaseEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedDate = date;

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                entry.Entity.ModifiedDate = date;
        }

        // 2. Save changes to database
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        // 3. Dispatch domain events
        var aggregates = GetTrackedAggregates();
        foreach (var aggregate in aggregates)
        {
            var events = aggregate.DomainEvents.ToList();
            aggregate.ClearDomainEvents();

            foreach (var domainEvent in events)
                await _dispatcher.DispatchAsync(domainEvent, cancellationToken);
        }

        return result;
    }

    public IReadOnlyList<AggregateRoot<Guid>> GetTrackedAggregates()
    {
        return _dbContext.ChangeTracker
                         .Entries<AggregateRoot<Guid>>()
                         .Select(e => e.Entity)
                         .ToList()
                         .AsReadOnly();
    }

    public void Dispose() => _dbContext.Dispose();
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
