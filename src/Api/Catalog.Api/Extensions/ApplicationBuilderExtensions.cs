using Catalog.Api.Middleware;

namespace Catalog.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds to the Application pipeline every custom middleware required by the system.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder" /> to whom will be registered the middleware</param>
    /// <returns><see cref="app"/></returns>
    public  static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandler>()
            .UseMiddleware<EntityNotFoundMiddleware>();
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.InjectStylesheet("/swagger-ui/custom.css");
        });

        return app;
    }
}
