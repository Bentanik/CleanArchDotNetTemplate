namespace _Project_.API;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAPIServices(builder.Configuration)
               .AddApplicationServices()
               .AddInfrastructureServices()
               .AddPersistenceServices(builder.Configuration);

        var app = builder.Build();

        app.ConfigureMiddleware();

        app.UseHttpsRedirection();

        app.Run();
    }
}