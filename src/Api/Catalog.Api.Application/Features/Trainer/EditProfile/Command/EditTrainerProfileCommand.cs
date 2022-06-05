using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Features.Trainer.Common.Dtos;
using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities.TrainerAggregate.Messages;
using Catalog.Api.EfCore.Context;
using Catalog.Shared.Enumerations.Trainer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Trainer.EditProfile.Command;

public class EditTrainerProfileCommand : ICommand<Unit>
{
    public int Id { set; get; }

    public string? Profession { get; set; }

    public string? Email { get; set; }

    public string? Bio { get; set; }

    public List<SocialNetworkAccountDto>? SocialNetworks { get; set; }
}

public class EditTrainerProfileCommandHandler : ICommandHandler<EditTrainerProfileCommand, Unit>
{
    private readonly CatalogContext _catalogContext;

    public EditTrainerProfileCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<Unit> Handle(EditTrainerProfileCommand command, CancellationToken cancellationToken)
    {
        var trainer = await _catalogContext.Trainer
            .Include(trainer => trainer.SocialNetworks)
            .FirstOrDefaultAsync(trainer => trainer.Id == command.Id, cancellationToken);

        if (trainer is null)
        {
            throw new EntityNotFoundException(command.Id, typeof(Domain.Entities.TrainerAggregate.Trainer));
        }

        var socialNetworks = command.SocialNetworks?.Select(socialNetwork => (SocialNetwork.FromValue(socialNetwork.SocialNetworkId), socialNetwork.Url));

        trainer.EditProfile(new EditProfileMessage(command.Profession, command.Bio!, command.Email, socialNetworks));

        await _catalogContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
