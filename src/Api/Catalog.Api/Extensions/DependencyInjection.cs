using Catalog.Api.Dependency;
using Catalog.Api.Middleware;
using FluentValidation.AspNetCore;

namespace Catalog.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDependencies(configuration).AddFluentValidation().AddCors();
        return services;
    }

    public static IServiceCollection AddFluentValidation(this IServiceCollection service)
    {
        return service
            .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(Program).Assembly))
            .AddTransient<IValidatorInterceptor, FluentValidationInterceptor>();
    }

    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }
}
