using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Delete;

public class TrainingDeletedEvent : IDomainEvent
{
    public int TrainingId { get; set; }

    public TrainingDeletedEvent(int trainingId)
    {
        TrainingId = trainingId;
    }
}