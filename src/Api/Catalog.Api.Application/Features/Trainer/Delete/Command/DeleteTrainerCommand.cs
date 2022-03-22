using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;

namespace Catalog.Api.Application.Features.Trainer.Delete.Command;

public class DeleteTrainerCommand : ICommand<bool>
{
    public int Id { get; init; }
}

public class DeleteTrainerCommandHandler : ICommandHandler<DeleteTrainerCommand, bool>
{
    private readonly CatalogContext _catalogContext;

    public DeleteTrainerCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<bool> Handle(DeleteTrainerCommand command, CancellationToken cancellationToken)
    {
        var trainer = await _catalogContext.Trainer.GetByIdAsync(command.Id, cancellationToken: cancellationToken);

        if (trainer is null)
        {
            return false;
        }

        trainer.DomainEvents.Add(new TrainerDeleteEvent(trainer.Id));
        _catalogContext.Trainer.Remove(trainer);
        return await _catalogContext.SaveChangesAsync(cancellationToken) > 0;
    }
}