using Catalog.Api.Application.MediatR;
using Catalog.Api.Endpoints;

namespace Catalog.Api.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandler> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            // See LoggedMediatRException for explanation.
            if (exception is not LoggedMediatRException)
            {
                logger.LogError(exception, "An uncaught exception occurred while executing: {url}", context.Request.Path);
            }

            context.Response.StatusCode = 500;
            var errorResponse = new ErrorResponse()
            {
                Status = 500,
                Errors = new[] { new Error("", "An expected error occurred while processing the request. Try again later.", null) }
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
