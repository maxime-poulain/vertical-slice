using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.Api.Endpoints;

public record ErrorResponse
{
    public int Status { get; init; }

    public IEnumerable<Error>? Errors { get; init; }
}

public record Error(string? PropertyName, string? ErrorMessage, object? AttemptedValue);

internal static class ErrorMapper
{
    public static ErrorResponse ToErrorResponse(this ValidationResult validationResult, int statusCode = StatusCodes.Status400BadRequest)
    {
        var errorResponse = new ErrorResponse();
        if (validationResult.IsValid)
        {
            return errorResponse;
        }

        return new ErrorResponse()
        {
            Errors = validationResult.Errors.Select(error => new Error(error.PropertyName, error.ErrorMessage, error.AttemptedValue)),
            Status = statusCode
        };
    }

    public static ErrorResponse ToErrorResponse(this ModelStateDictionary modelState)
    {
        // We know for sure that is we are here this means the framework acknowledge there was validation error(s).
        var errors = new List<Error>();
        foreach (var errorEntriesByPropertyName in modelState)
        {
            errors.AddRange(errorEntriesByPropertyName.Value.Errors.Select(e => new Error(errorEntriesByPropertyName.Key, e.ErrorMessage, errorEntriesByPropertyName.Value.AttemptedValue)));
        }

        return new ErrorResponse()
        {
            Status = 400,
            Errors = errors
        };
    }
}
