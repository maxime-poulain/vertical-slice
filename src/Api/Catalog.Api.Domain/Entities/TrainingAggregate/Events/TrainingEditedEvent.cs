using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Domain.Entities.TrainingAggregate.Events;

public class TrainingEditedEvent : IDomainEvent
{
    public Training Training { get; }

    public TrainingEditedEvent(Training training)
    {
        Training = training;
    }
}
