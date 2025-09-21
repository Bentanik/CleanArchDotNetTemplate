namespace _Project_.API.DependencyInjection.Extensions;

public static class MiddlewareExtensions
{
  public static void ConfigureMiddleware(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
    }

    app.ConfigureSwagger();

    app.MapCarter();

    // app.NewVersionedApi("User")
    //   .MapUserApiV1();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();
  }
}