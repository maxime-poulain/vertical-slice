using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Enumerations.Training;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;

namespace Catalog.Api.Application.Features.Training.Create.Command;

public class CreateTrainingCommand : ICommand<CreatedTrainingDto>
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Goal { get; set; }

    public int TrainingTypeId { get; set; }
}

public class CreateTrainingCommandHandler : ICommandHandler<CreateTrainingCommand, CreatedTrainingDto>
{
    private readonly CatalogContext _catalogContext;

    public CreateTrainingCommandHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<CreatedTrainingDto> Handle(CreateTrainingCommand command, CancellationToken cancellationToken)
    {
        // Setup the training entity to add in the database.
        var training = MakeTraining(command);

        // Attach the training creation event.
        training.DomainEvents.Add(new TrainingCreatedEvent(training));

        // Insert into the database the training.
        await _catalogContext.InsertAsync(training, cancellationToken);

        return new CreatedTrainingDto()
        {
            TrainingId = training.Id
        };
    }

    private Domain.Entities.Training MakeTraining(CreateTrainingCommand request)
    {
        return new Domain.Entities.Training(request.Title!,
            request.Description!,
            request.Goal!,
            TrainingType.FromValue(request.TrainingTypeId));
    }
}