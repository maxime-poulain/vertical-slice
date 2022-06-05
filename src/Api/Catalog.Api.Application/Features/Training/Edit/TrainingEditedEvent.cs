using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Edit;

public class TrainingEditedEvent : IDomainEvent
{
    public Domain.Entities.TrainingAggregate.Training Training { get; }

    public TrainingEditedEvent(Domain.Entities.TrainingAggregate.Training training)
    {
        Training = training;
    }
}
