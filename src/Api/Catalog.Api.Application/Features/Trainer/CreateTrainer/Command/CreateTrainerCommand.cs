using Catalog.Api.Application.Features.Trainer.Common.Dtos;
using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities.TrainerAggregate.Messages;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;
using Catalog.Shared.Enumerations.Trainer;

namespace Catalog.Api.Application.Features.Trainer.CreateTrainer.Command;

public class CreateTrainerCommand : ICommand<TrainerCreationResult>
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Bio { get; set; }

    public string? Profession { get; set; }

    public List<SocialNetworkAccountDto>? SocialNetworks { get; set; }
}

public class CreateTrainerCommandHandler : ICommandHandler<CreateTrainerCommand, TrainerCreationResult>
{
    private readonly CatalogContext _catalogContext;

    public CreateTrainerCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<TrainerCreationResult> Handle(CreateTrainerCommand command, CancellationToken cancellationToken)
    {
        var trainer = MakeTrainer(command);

        trainer.DomainEvents.Add(new TrainerCreatedEvent(trainer));

        await _catalogContext.InsertAsync(trainer, cancellationToken);

        return new TrainerCreationResult()
        {
            TrainerId = trainer.Id
        };
    }

    private Domain.Entities.TrainerAggregate.Trainer MakeTrainer(CreateTrainerCommand command)
    {
        var socialNetworks = command.SocialNetworks?.Select(s => (SocialNetwork.FromValue(s.SocialNetworkId), s.Url));
        var message = new CreateTrainerMessage(command.Firstname!, command.Lastname!, command.Profession, command.Bio, command.Email, socialNetworks);
        return Domain.Entities.TrainerAggregate.Trainer.Create(message);
    }
}
