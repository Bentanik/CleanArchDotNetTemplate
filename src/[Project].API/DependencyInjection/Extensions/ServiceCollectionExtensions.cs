namespace _Project_.API.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddConfigurationAppSetting
     (this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwagger();

        return services;
    }

    private static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                typeof(Application.AssemblyReference).Assembly,
                typeof(Infrastructure.AssemblyReference).Assembly
            ))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventDispatcherBehavior<,>))
            .AddValidatorsFromAssembly(Contracts.AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }

    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConfigurationAppSetting(configuration);

        services.AddConfigureMediatR();

        services.AddCarter();

        services.AddScoped<ExceptionHandlingMiddleware>();

        services.AddSwaggerServices();

        services
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}