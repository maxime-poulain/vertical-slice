using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Enumerations.Trainer;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Trainer.CreateTrainer.Command;

public class CreateTrainerCommand : ICommand<TrainerCreationResult>
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Bio { get; set; }

    public int TrainerSkillLevelId { get; set; }
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

        await InsertTrainerAsync(cancellationToken, trainer);

        return new TrainerCreationResult()
        {
            TrainerId = trainer.Id
        };
    }

    private Domain.Entities.Trainer MakeTrainer(CreateTrainerCommand command)
    {
        var trainer = new Domain.Entities.Trainer(command.Firstname!,
            command.Lastname!,
            command.Bio!,
            TrainerSkillLevel.FromValue(command.TrainerSkillLevelId));

        return trainer;
    }

    private async Task InsertTrainerAsync(CancellationToken cancellationToken, Domain.Entities.Trainer trainer)
    {
        _catalogContext.Trainer.Add(trainer);
        await _catalogContext.SaveChangesAsync(cancellationToken);
    }
}