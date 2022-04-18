using FluentValidation.Results;

namespace Catalog.Api.Features;

/// <inheritdoc cref="IValidationResultFeature"/>
public class ValidationResultFeature : IValidationResultFeature
{
    public ValidationResult ValidationResult { get; }

    public ValidationResultFeature(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }
}

/// <summary>
/// Provides the <see cref="FluentValidation.Results.ValidationResult" /> associated to the current request.
/// </summary>
public interface IValidationResultFeature
{
    public ValidationResult ValidationResult { get; }
}
