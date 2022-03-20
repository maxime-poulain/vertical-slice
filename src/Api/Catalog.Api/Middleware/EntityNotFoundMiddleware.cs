using Catalog.Api.Application.Common.Exceptions;

namespace Catalog.Api.Middleware;

/// <summary>
/// A middleware that enable capabilities to handle <see cref="EntityNotFoundException" /> uncaught exceptions.
/// </summary>
public class EntityNotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public EntityNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException)
        {
            SetResponseToNotFound(context);
        }
    }

    private static void SetResponseToNotFound(HttpContext context)
    {
        context.Response.Clear();
        context.Response.ContentLength = 0;
        context.Response.StatusCode    = StatusCodes.Status404NotFound;
    }
}