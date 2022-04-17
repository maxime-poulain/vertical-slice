using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.EditProfile;

public class TrainerProfileEditedEvent : IDomainEvent
{
    public int Id { get; }

    public TrainerProfileEditedEvent(int trainerId)
    {
        Id = trainerId;
    }
}
