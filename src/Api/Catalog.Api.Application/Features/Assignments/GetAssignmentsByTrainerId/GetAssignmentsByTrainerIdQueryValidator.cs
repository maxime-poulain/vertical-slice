using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using FluentValidation;

namespace Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainerId;

public class GetAssignmentsByTrainerIdQueryValidator : AbstractValidator<GetAssignmentsByTrainerIdQuery>
{
    public GetAssignmentsByTrainerIdQueryValidator(CatalogContext catalogContext)
    {
        RuleFor(query => query.Id).ThrowErrorIfEntityDoesNotExistAsync<GetAssignmentsByTrainerIdQuery, Domain.Entities.Trainer>(catalogContext);
    }
}