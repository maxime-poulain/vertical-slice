using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Shared.Enumerations.Training;
using FluentValidation;

namespace Catalog.Api.Application.Features.Training.Common.CreateEdit;

public abstract class CreateEditTrainingCommonValidator<T, TResponse> : AbstractValidator<T> where T : CreateEditTrainingCommonCommand<TResponse>
{
    protected CreateEditTrainingCommonValidator()
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

        RuleFor(command => command.Topics)
            .AllEnumerationExists<T, Topic>();

        RuleFor(command => command.Audiences)
            .AllEnumerationExists<T, Audience>();

        RuleFor(command => command.VatJustifications)
            .AllEnumerationExists<T, VatJustification>();

        RuleFor(command => command.Attendances)
            .AllEnumerationExists<T, Attendance>();
    }
}