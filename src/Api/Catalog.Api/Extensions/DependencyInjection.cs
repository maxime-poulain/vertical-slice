using Catalog.Api.Dependency;
using Catalog.Api.Middleware;
using FluentValidation.AspNetCore;

namespace Catalog.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDependencies(configuration).AddFluentValidation();
        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection service)
    {
        return service
            .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(Program).Assembly))
            .AddTransient<IValidatorInterceptor, FluentValidationInterceptor>();
    }
}