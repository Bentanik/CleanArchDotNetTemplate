namespace _Project_.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();

        if (databaseSettings == null || string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
            throw new InvalidOperationException($"Database settings are not configured. Make sure '{DatabaseSettings.SectionName}' section exists and has a valid ConnectionString.");

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseSqlite(databaseSettings.ConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConfiguration(configuration);
        return services;
    }
}