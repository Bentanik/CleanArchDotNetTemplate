using _Project_.Application.Interfaces.Repositories;

namespace _Project_.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        if (databaseSettings == null || string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
        {
            throw new InvalidOperationException(
                $"Database settings are not configured. Make sure the '{DatabaseSettings.SectionName}' section exists and has a valid ConnectionString.");
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(databaseSettings.ConnectionString);
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>))
        .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>))
        .AddScoped<IExampleAggregateCommandRepository, ExampleAggregateCommandRepository>()
        .AddScoped<IExampleAggregateQueryRepository, ExampleAggregateQueryRepository>();

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConfiguration(configuration);
        services.AddRepositories();

        return services;
    }
}