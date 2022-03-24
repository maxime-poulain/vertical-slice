using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Enumerations.Training;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Training.Common.CreateEdit;

public abstract class CreateEditTrainingCommonCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse> where TCommand : CreateEditTrainingCommonCommand<TResponse>
{
    protected readonly CatalogContext CatalogContext;

    /// <summary>
    /// Prepares the training for the execution of the command.
    /// Since this base command class can be inherited for creation and edition of a training,
    /// the logic to retrieve the training is different.
    /// We need to make a new entity for the creation but retrieval from the database for edition.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    protected abstract Task<Domain.Entities.Training> GetOrMakeTrainingAsync(TCommand command);

    /// <summary>
    /// Returns the <see cref="IDomainEvent" /> that should be published after the save.
    /// </summary>
    /// <param name="training">The <see cref="Domain.Entities.Training" /> to whom this event is linked.</param>
    /// <returns>A domain event to be published after a successful save.</returns>
    protected abstract IDomainEvent DomainEventForCurrentOperation(Domain.Entities.Training training);

    /// <summary>
    /// Build the result of the current operation.
    /// </summary>
    /// <param name="training">The <see cref="Domain.Entities.Training" /> on whose the operation was applied.</param>
    /// <returns>The result of the command.</returns>
    protected abstract TResponse MakeResult(Domain.Entities.Training training);

    protected CreateEditTrainingCommonCommandHandler(CatalogContext catalogContext)
    {
        CatalogContext = catalogContext;
    }

    public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var training = await GetOrMakeTrainingAsync(command);
        UpdateFields(training, command);
        training.DomainEvents.Add(DomainEventForCurrentOperation(training));
        if (training.Id == default)
        {
            CatalogContext.Training.Add(training);
        }

        await CatalogContext.SaveChangesAsync(cancellationToken);
        return MakeResult(training);

    }
    private void UpdateFields(Domain.Entities.Training training, CreateEditTrainingCommonCommand<TResponse> command)
    {
        // Those fields cannot be null, the validation asserts that.
        training.ChangeTitle(command.Title!);
        training.ChangeDescription(command.Description!);
        training.ChangeType(TrainingType.FromValue(command.TrainingTypeId));
        training.ChangeGoal(command.Goal!);

    }
}