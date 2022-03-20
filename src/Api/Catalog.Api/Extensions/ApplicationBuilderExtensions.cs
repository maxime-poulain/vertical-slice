using System;
using System.Collections.Generic;
namespace Catalog.Api.Extensions;

public static class ApplicationBuilderExtensions
{
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