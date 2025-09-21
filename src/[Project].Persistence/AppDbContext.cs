namespace _Project_.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
           => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ExampleAggregate> Examples { get; set; }
    public DbSet<ExampleItemEntity> ExampleItems { get; set; }
}