namespace _Project_.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection RegisterDomainEventServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }

    private static IServiceCollection RegisterIdempotencyStoreServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRequestIdempotencyStore, InMemoryIdempotencyStore>();
        return services;
    }

    private static IServiceCollection RegisterMessageBusServices(this IServiceCollection services)
    {
        services.AddScoped<IMessageBus, MediatRMessageBus>();

        return services;
    }

    private static IServiceCollection RegisterRequestContextServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<HttpRequestContext>();
        services.AddScoped<IRequestContext>(sp =>
        {
            var http = sp.GetService<HttpRequestContext>();
            return new CompositeRequestContext(http);
        });
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
            .RegisterExternalServices()
            .RegisterMessageBusServices()
            .RegisterIdempotencyStoreServices()
            .RegisterRequestContextServices();

        return services;
    }
}