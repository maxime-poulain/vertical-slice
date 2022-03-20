using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Create;

public class TrainingCreatedEvent : IDomainEvent
{
    public Domain.Entities.Training Training { get; set; }

    public TrainingCreatedEvent(Domain.Entities.Training training)
    {
        Training = training;
    }
}