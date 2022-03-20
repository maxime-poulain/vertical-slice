using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.Domain.Enumerations.Training;
using FluentValidation;

namespace Catalog.Api.Application.Features.Training.Create.Command;

public class CreateTrainingCommandValidator : AbstractValidator<CreateTrainingCommand>
{
    public CreateTrainingCommandValidator()
    {
        RuleFor(createTrainingCommand => createTrainingCommand.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MinimumLength(5)
            .WithMessage("Title must be at least 5 characters long");

        RuleFor(createTrainingCommand => createTrainingCommand.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MinimumLength(30)
            .WithMessage("Description must be at least 30 characters long")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters");

        RuleFor(command => command.Goal)
            .NotEmpty()
            .WithMessage("Goal is required")
            .MinimumLength(30)
            .WithMessage("Goal must be at least 30 characters long")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters");

        RuleFor(createTrainingCommand => createTrainingCommand.TrainingTypeId)
            .NotEmpty()
            .EnumerationExists<CreateTrainingCommand, TrainingType>()
            .WithMessage(createTrainingCommand => $"Unknown training type {createTrainingCommand.TrainingTypeId}");
    }
}