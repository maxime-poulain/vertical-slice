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
            if (exception is not LoggedMediatRException)
                logger.LogError(exception, "An uncaught exception occurred while executing: {url}", context.Request.Path);

            context.Response.StatusCode = 500;
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new Error() { ErrorCode = "-9999", ErrorMessage = "Unexpected server error" });
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
