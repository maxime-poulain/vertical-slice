using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Create.EvenHandlers;

public class LogWhenTrainingCreatedEventHandler : IDomainEventHandler<TrainingCreatedEvent>
{
    public Task Handle(TrainingCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Do some pretty logging.
        return Task.CompletedTask;
    }
}