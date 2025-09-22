namespace _Project_.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection RegisterDomainEventServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }

    private static IServiceCollection RegisterExternalServices(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services
            .RegisterDomainEventServices()
            .RegisterExternalServices();

        return services;
    }
}