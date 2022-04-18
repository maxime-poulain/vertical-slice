using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.Delete;

public class TrainerDeleteEvent : IDomainEvent
{
    private readonly int _trainerId;

    public TrainerDeleteEvent(int trainerId)
    {
        _trainerId = trainerId;
    }
}
