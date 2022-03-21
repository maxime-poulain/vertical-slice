namespace Catalog.Api.Extensions;

public static class SwaggerRegistration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSwaggerGen(options => options.EnableAnnotations());
    }
}