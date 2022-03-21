using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using FluentValidation;

namespace Catalog.Api.Application.Features.Training.GetById;

public class GetTrainingByIdQueryValidator : AbstractValidator<GetTrainingByIdQuery>
{
    public GetTrainingByIdQueryValidator(CatalogContext catalogContext)
    {
        RuleFor(command => command.Id)
            .ThrowErrorIfEntityDoesNotExistAsync<GetTrainingByIdQuery, Domain.Entities.Training>(catalogContext);
    }
}