using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Shared.Enumerations.Trainer;
using FluentValidation;

namespace Catalog.Api.Application.Features.Trainers.CreateTrainer.Command;

public class CreateTrainerCommandValidator : AbstractValidator<CreateTrainerCommand>
{
    public CreateTrainerCommandValidator()
    {
        RuleFor(command => command.Firstname)
            .NotEmpty()
            .WithMessage("Firstname is required");

        RuleFor(command => command.Lastname)
            .NotEmpty()
            .WithMessage("Lastname is required");

        RuleFor(command => command.Bio)
            .MinimumLength(30)
            .WithMessage("Bio must be at least 30 characters long")
            .MaximumLength(500)
            .WithMessage("Bio must not exceed 500 characters");

        RuleFor(command => command.SocialNetworks!.Select(socialNetworkAccountDto => socialNetworkAccountDto.SocialNetworkId))
            .AllEnumerationExists<CreateTrainerCommand, SocialNetwork>()
            .WithName(command => nameof(command.SocialNetworks))
            .When(command => command.SocialNetworks is not null);
    }
}
