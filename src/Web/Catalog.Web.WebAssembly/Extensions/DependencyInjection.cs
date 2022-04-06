using Catalog.Shared.HttpClients.Catalog;
using Sotsera.Blazor.Toaster.Core.Models;

namespace Catalog.Web.WebAssembly.Extensions;

public static class DependencyInjection
{
    private const string CatalogHttpClientName = nameof(CatalogHttpClientName);
    private const string CurrentHostClientName = nameof(CurrentHostClientName);

    public static IServiceCollection AddDependencies( this IServiceCollection services, IConfiguration configuration, string hostEnvironmentBaseAddress)
    {
        return services
            .AddToaster()
            .AddHttpClients(configuration, hostEnvironmentBaseAddress)
            .AddCatalogHttpClients();
    }

    private static IServiceCollection AddToaster(this IServiceCollection services)
    {
        return services.AddToaster(options =>
        {
            options.PositionClass  = Defaults.Classes.Position.TopRight;
            options.MaximumOpacity = 80;
        });
    }

    private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, string hostEnvironmentBaseAddress)
    {
        services.AddHttpClient(CatalogHttpClientName, client =>
        {
            client.BaseAddress = new Uri(configuration["Api:Address"]);
        });

        services.AddHttpClient(CurrentHostClientName, client =>
        {
            client.BaseAddress = new Uri(hostEnvironmentBaseAddress);
        });

        services.AddCatalogHttpClients();
        return services;
    }

    private static IServiceCollection AddCatalogHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<ITrainingClient, TrainingClient>(CatalogHttpClientName);
        services.AddHttpClient<ITrainerClient, TrainerClient>(CatalogHttpClientName);

        return services;
    }
}
