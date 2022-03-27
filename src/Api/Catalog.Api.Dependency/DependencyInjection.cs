using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddEfCore(configuration)
            .AddMediatR()
            .AddFluentValidation();
    }
}