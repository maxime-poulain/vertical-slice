using Catalog.Api.Application.Extensions.FluentValidationExtensions;
using Catalog.Api.EfCore.Context;
using Catalog.Shared.Enumerations.Trainer;
using FluentValidation;

namespace Catalog.Api.Application.Features.Trainers.EditProfile.Command;

public class EditTrainerProfileCommandValidator : AbstractValidator<EditTrainerProfileCommand>
{
    public EditTrainerProfileCommandValidator(CatalogContext catalogContext)
    {
        RuleFor(command => command.Id)
            .ThrowErrorIfEntityDoesNotExistAsync(catalogContext.Trainer);

        RuleFor(command => command.Bio)
            .NotEmpty()
            .MinimumLength(30)
            .MaximumLength(1000);

        RuleFor(command => command.SocialNetworks!.Select(x => x.SocialNetworkId))
            .AllEnumerationExists<EditTrainerProfileCommand, SocialNetwork>()
            .WithName(command => nameof(command.SocialNetworks))
            .When(command => command.SocialNetworks is not null);
    }
}
