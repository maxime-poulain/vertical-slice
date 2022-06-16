using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Catalog.Api.Application.MediatR.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validationResults = new List<ValidationResult>();
        foreach (var validator in _validators)
        {
            validationResults.Add(await validator.ValidateAsync(request, cancellationToken));
        }

        var errors = validationResults.SelectMany(result => result.Errors);
        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
