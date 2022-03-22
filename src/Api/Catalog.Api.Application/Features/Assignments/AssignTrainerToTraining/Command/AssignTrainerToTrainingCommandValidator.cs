using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using FluentValidation;

namespace Catalog.Api.Application.Features.Assignments.AssignTrainerToTraining.Command;

public class AssignTrainerToTrainingCommandValidator : AbstractValidator<AssignTrainerToTrainingCommand>
{
    public AssignTrainerToTrainingCommandValidator(CatalogContext catalogContext)
    {
        RuleFor(command => command.TrainerId)
            .EntityExistsAsync<AssignTrainerToTrainingCommand, Domain.Entities.Trainer>(catalogContext)
            .WithMessage((_, trainerId) => $"Trainer with id {trainerId} was not found");

        RuleFor(command => command.TrainingId)
            .EntityExistsAsync<AssignTrainerToTrainingCommand, Domain.Entities.Training>(catalogContext)
            .WithMessage((_, trainingId) => $"Training with id {trainingId} was not found");
    }
}