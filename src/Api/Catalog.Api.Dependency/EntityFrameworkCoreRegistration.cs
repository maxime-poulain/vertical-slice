using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Dependency;

public static class EntityFrameworkCoreRegistration
{
    private const string Sqlite    = "sqlite";
    private const string InMemory  = "inmemory";
    private const string SqlServer = "sqlserver";

    public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        var dbms = configuration["Provider"]?.ToLowerInvariant() ??
                   throw new InvalidOperationException("No provider specified");

        return dbms switch
        {
            Sqlite    => services.AddSqliteEfCore(configuration),
            InMemory  => services.AddInMemoryEfCore(),
            SqlServer => services.AddSqlServerEfCore(configuration),
            _         => throw new InvalidOperationException($"Unknown dbms: `{dbms}`")
        };
    }

    private static IServiceCollection AddSqliteEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<CatalogContext>(options => options.UseSqlite(configuration.GetConnectionString("Catalog")));
    }

    private static IServiceCollection AddInMemoryEfCore(this IServiceCollection services)
    {
        return services.AddDbContext<CatalogContext>(options => options.UseInMemoryDatabase("Catalog"));
    }

    private static IServiceCollection AddSqlServerEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<CatalogContext>(options => options.UseSqlServer(configuration.GetConnectionString("Catalog")));
    }
}