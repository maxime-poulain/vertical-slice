using Catalog.Api.Domain.Entities.Base;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Application.Features.Trainers.EditProfile.EventHandlers;

public class TrainerProfileEditedEventHandler : IDomainEventHandler<TrainerProfileEditedEvent>
{
    private readonly ILogger<TrainerProfileEditedEventHandler> _logger;

    public TrainerProfileEditedEventHandler(ILogger<TrainerProfileEditedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TrainerProfileEditedEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Profile of Trainer {id} edited", @event.Id);
        return Task.CompletedTask;
    }
}
