using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.CreateTrainer;

public class TrainerCreatedEvent : IDomainEvent
{
    public Domain.Entities.Trainer Trainer { get; init; }

    public TrainerCreatedEvent(Domain.Entities.Trainer trainer)
    {
        Trainer = trainer;
    }
}