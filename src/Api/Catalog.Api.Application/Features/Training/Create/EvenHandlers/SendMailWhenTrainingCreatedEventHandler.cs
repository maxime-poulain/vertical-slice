using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Features.Training.Create.EvenHandlers;

public class SendMailWhenTrainingCreatedEventHandler : IDomainEventHandler<TrainingCreatedEvent>
{
    public Task Handle(TrainingCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Use the  Email service to do some stuff.

        return Task.CompletedTask;
    }
}