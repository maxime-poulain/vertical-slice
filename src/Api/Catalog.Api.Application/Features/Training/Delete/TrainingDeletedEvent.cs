using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Delete;

public class TrainingDeletedEvent : IDomainEvent
{
    public Guid TrainingId { get; set; }

    public TrainingDeletedEvent(Guid trainingId)
    {
        TrainingId = trainingId;
    }
}
