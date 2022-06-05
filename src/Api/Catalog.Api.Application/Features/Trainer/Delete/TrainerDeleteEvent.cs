using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.Delete;

public class TrainerDeleteEvent : IDomainEvent
{
    private Guid TrainerId { get; }

    public TrainerDeleteEvent(Guid trainerId)
    {
        TrainerId = trainerId;
    }
}
