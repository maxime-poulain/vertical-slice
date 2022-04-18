using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Extensions;
using Catalog.Api.Endpoints;

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
        catch (EntityNotFoundException exception)
        {
            SetResponseToNotFound(context, exception);
        }
    }

    private static void SetResponseToNotFound(HttpContext context, EntityNotFoundException entityNotFoundException)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.WriteAsJsonAsync(new ErrorResponse()
        {
            Status = StatusCodes.Status404NotFound,
            Errors = new[]
            {
                new Error("",
                    $"Entity {entityNotFoundException.EntityType.GetGenericTypeName()} with id {entityNotFoundException.EntityId} not found",
                    entityNotFoundException.EntityId)
            }
        });
    }
}
