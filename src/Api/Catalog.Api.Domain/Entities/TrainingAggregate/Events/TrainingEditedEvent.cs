using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Domain.Entities.TrainingAggregate.Events;

public class TrainingEditedEvent : IDomainEvent
{
    public Domain.Entities.TrainingAggregate.Training Training { get; }

    public TrainingEditedEvent(Domain.Entities.TrainingAggregate.Training training)
    {
        Training = training;
    }
}
