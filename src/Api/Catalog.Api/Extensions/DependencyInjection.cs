using Catalog.Api.Dependency;

namespace Catalog.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDependencies(configuration).AddCors();
        return services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }
}
