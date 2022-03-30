using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using FluentValidation;

namespace Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainingId;

public class GetAssignmentsByTrainingIdQueryValidator : AbstractValidator<GetAssignmentsByTrainingIdQuery>
{
    public GetAssignmentsByTrainingIdQueryValidator(CatalogContext catalogContext)
    {
        RuleFor(command => command.Id)
            .ThrowErrorIfEntityDoesNotExistAsync(catalogContext.Training);
    }
}