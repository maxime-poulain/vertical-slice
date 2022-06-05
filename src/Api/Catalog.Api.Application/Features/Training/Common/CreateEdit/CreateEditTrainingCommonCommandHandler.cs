using Catalog.Api.Domain.CQS;
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
    protected abstract Task<Domain.Entities.TrainingAggregate.Training> GetTrainingAccordinglyToCommandAsync(TCommand command);

    /// <summary>
    /// Build the result of the current operation.
    /// </summary>
    /// <param name="training">The <see cref="Training" /> on whose the operation was applied.</param>
    /// <returns>The result of the command.</returns>
    protected abstract TResponse MakeResult(Domain.Entities.TrainingAggregate.Training training);

    protected CreateEditTrainingCommonCommandHandler(CatalogContext catalogContext)
    {
        CatalogContext = catalogContext;
    }

    public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var training = await GetTrainingAccordinglyToCommandAsync(command);
        training.Edit(command);
        if (training.Id == default)
        {
            CatalogContext.Training.Add(training);
        }

        await CatalogContext.SaveChangesAsync(cancellationToken);
        return MakeResult(training);
    }
}
