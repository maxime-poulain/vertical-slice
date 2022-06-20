using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using FluentValidation;

namespace Catalog.Api.Application.Features.Trainings.Delete.Command;

public class DeleteTrainingCommandValidator : AbstractValidator<DeleteTrainingCommand>
{
    public DeleteTrainingCommandValidator(CatalogContext catalogContext)
    {
        RuleFor(command => command.Id)
            .ThrowErrorIfEntityDoesNotExistAsync<DeleteTrainingCommand, Domain.Entities.TrainingAggregate.Training>(catalogContext);
    }
}
