using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;
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

    /// <summary>
    /// Creates the database if the underlying provider is sqlite and a seeds a default data.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <param name="configuration"></param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static async Task<IApplicationBuilder> EnsureCreationOfDatabaseAsync(this IApplicationBuilder app, IConfiguration configuration)
    {
        // For the time being this is okay but later on this will most likely require refactoring towards a service or even a worker service.

        // If underlying provider is sqlite we would like to have the database existing for easier tests.
        if (configuration["Provider"].Equals("sqlite", StringComparison.OrdinalIgnoreCase))
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
                await context.Database.EnsureCreatedAsync();
                await context.SeedDefaultTrainerAsync();
            }
        }

        return app;
    }
}
