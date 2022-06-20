using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainings.Create;

public class TrainingCreatedEvent : IDomainEvent
{
    public Domain.Entities.TrainingAggregate.Training Training { get; set; }

    public TrainingCreatedEvent(Domain.Entities.TrainingAggregate.Training training)
    {
        Training = training;
    }
}
