using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Assignments.AssignTrainerToTraining.EventHandlers;

public class TrainingAssignmentCreatedEventHandler : IDomainEventHandler<TrainingAssignmentCreatedEvent>
{
    public Task Handle(TrainingAssignmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
