using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Delete.EventHandles;

public class TrainingDeletedEventHandler : IDomainEventHandler<TrainingDeletedEvent>
{
    public Task Handle(TrainingDeletedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}