using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;

namespace Catalog.Api.Application.Features.Training.Delete.Command;

public class DeleteTrainingCommand : ICommand<bool>
{
    public int Id { get; init; }
}

public class DeleteTrainingCommandHandler : ICommandHandler<DeleteTrainingCommand, bool>
{
    private readonly CatalogContext _catalogContext;

    public DeleteTrainingCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<bool> Handle(DeleteTrainingCommand command, CancellationToken cancellationToken)
    {
        var training = await _catalogContext.Training.GetByIdAsync(command.Id, cancellationToken: cancellationToken);
        if (training is null)
        {
            return false;
        }

        await RemoveFromDatabaseAsync(training, cancellationToken);
        return true;
    }

    private async Task RemoveFromDatabaseAsync(Domain.Entities.Training training, CancellationToken cancellationToken)
    {
        _catalogContext.Training.Remove(training);
        await _catalogContext.SaveChangesAsync(cancellationToken);
    }
}