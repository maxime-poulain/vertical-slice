using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.EditProfile;

public class TrainerProfileEditedEvent : IDomainEvent
{
    public Guid Id { get; }

    public TrainerProfileEditedEvent(Guid trainerId)
    {
        Id = trainerId;
    }
}
