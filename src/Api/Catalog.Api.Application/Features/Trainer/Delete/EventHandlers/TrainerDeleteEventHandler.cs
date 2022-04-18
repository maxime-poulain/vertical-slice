using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Trainer.Delete.EventHandlers;

public class TrainerDeleteEventHandler : IDomainEventHandler<TrainerDeleteEvent>
{
    public Task Handle(TrainerDeleteEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
