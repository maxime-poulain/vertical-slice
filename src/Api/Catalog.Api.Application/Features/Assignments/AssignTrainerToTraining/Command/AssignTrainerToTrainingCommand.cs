using System.Linq.Expressions;
using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Assignments.AssignTrainerToTraining.Command;

public class AssignTrainerToTrainingCommand : ICommand<AssignmentDto>
{
    public int TrainingId { get; init; }

    public int TrainerId { get; init; }
}

public class AssignTrainerToTrainingCommandHandler : ICommandHandler<AssignTrainerToTrainingCommand, AssignmentDto>
{
    private readonly CatalogContext _catalogContext;

    public AssignTrainerToTrainingCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<AssignmentDto> Handle(AssignTrainerToTrainingCommand command, CancellationToken cancellationToken)
    {
        // At this point we don't know if there is already an existing assignment.
        // However thanks to the validation we are sure that the trainer and training exist.
        var existingAssignment = await GetExistingAssignmentAsync(command, cancellationToken) ??
                                 await CreateNewAssignmentAsync(command, cancellationToken);

        return new AssignmentDto()
        {
            TrainerId  = existingAssignment.TrainerId,
            TrainingId = existingAssignment.TrainingId
        };
    }

    private async Task<TrainingAssignment> CreateNewAssignmentAsync(AssignTrainerToTrainingCommand command, CancellationToken cancellationToken)
    {
        var (training, trainer) = await GetTrainerAndTrainingByTheirIdsAsync(command, cancellationToken);

        var assignment = training.Assign(trainer);

        training.DomainEvents.Add(new TrainingAssignmentCreatedEvent(assignment));

        await _catalogContext.SaveChangesAsync(cancellationToken);
        return assignment;
    }

    private async Task<TrainingAssignment?> GetExistingAssignmentAsync(AssignTrainerToTrainingCommand command, CancellationToken cancellationToken)
    {
        Expression<Func<TrainingAssignment, bool>> existingPredicate = assignment => assignment.TrainerId == command.TrainerId && assignment.TrainingId == command.TrainingId;
        var existingAssignment = await _catalogContext.TrainingAssignment.FirstOrDefaultAsync(existingPredicate, cancellationToken);
        return existingAssignment;
    }

    private async Task<(Domain.Entities.Training training, Domain.Entities.Trainer trainer)> GetTrainerAndTrainingByTheirIdsAsync(AssignTrainerToTrainingCommand command, CancellationToken cancellationToken)
    {
        var trainer  = await _catalogContext.Trainer.FirstOrDefaultAsync(trainer => trainer.Id == command.TrainerId, cancellationToken);
        var training = await _catalogContext.Training.FirstOrDefaultAsync(training => training.Id == command.TrainingId, cancellationToken);
        return (training!, trainer!);
    }
}
