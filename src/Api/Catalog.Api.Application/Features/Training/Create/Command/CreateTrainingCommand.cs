using Catalog.Api.Application.Features.Training.Common.CreateEdit;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Enumerations.Training;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Training.Create.Command;

public class CreateTrainingCommand : CreateEditTrainingCommonCommand<CreatedTrainingDto>
{
}

public class CreateTrainingCommandHandler : CreateEditTrainingCommonCommandHandler<CreateTrainingCommand, CreatedTrainingDto>
{

    public CreateTrainingCommandHandler(CatalogContext catalogContext) : base(catalogContext)
    {

    }

    protected override Task<Domain.Entities.Training> GetOrMakeTrainingAsync(CreateTrainingCommand command)
    {
        var training = new Domain.Entities.Training(command.Title!,
            command.Description!,
            command.Goal!);

        return Task.FromResult(training);
    }

    protected override IDomainEvent DomainEventForCurrentOperation(Domain.Entities.Training training)
    {
        return new TrainingCreatedEvent(training);
    }

    protected override CreatedTrainingDto MakeResult(Domain.Entities.Training training)
    {
        return new CreatedTrainingDto()
        {
            TrainingId = training.Id
        };
    }
}