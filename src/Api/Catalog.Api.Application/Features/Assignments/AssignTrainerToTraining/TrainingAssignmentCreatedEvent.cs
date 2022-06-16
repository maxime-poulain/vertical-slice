using Ardalis.GuardClauses;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Entities.TrainingAggregate;

namespace Catalog.Api.Application.Features.Assignments.AssignTrainerToTraining;

public class TrainingAssignmentCreatedEvent : IDomainEvent
{
    public TrainingAssignment TrainingAssignment { get; init; }

    public TrainingAssignmentCreatedEvent(TrainingAssignment trainingAssignment)
    {
        TrainingAssignment = Guard.Against.Null(trainingAssignment);
    }
}
