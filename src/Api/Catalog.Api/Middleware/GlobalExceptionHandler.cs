using Catalog.Api.Application.MediatR;
using Catalog.Api.Endpoints;
using FluentValidation;

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
        catch (ValidationException validationException)
        {
            await HandleValidationFailures(context, validationException);
        }
        catch (Exception exception)
        {
            await HandleUncaughtException(context, logger, exception);
        }
    }

    private static async Task HandleValidationFailures(HttpContext context, ValidationException validationException)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var response = new ErrorResponse()
        {
            Status = StatusCodes.Status400BadRequest,
            Errors = validationException.Errors.ToErrors()
        };
        await context.Response.WriteAsJsonAsync(response);
    }

    private static async Task HandleUncaughtException(HttpContext context, ILogger<GlobalExceptionHandler> logger, Exception exception)
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
