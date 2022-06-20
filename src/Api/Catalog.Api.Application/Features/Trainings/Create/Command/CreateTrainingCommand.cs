using Catalog.Api.Application.Features.Trainings.Common.CreateEdit;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Trainings.Create.Command;

public class CreateTrainingCommand : CreateEditTrainingCommonCommand<CreatedTrainingDto>
{
}

public class CreateTrainingCommandHandler : CreateEditTrainingCommonCommandHandler<CreateTrainingCommand, CreatedTrainingDto>
{
    public CreateTrainingCommandHandler(CatalogContext catalogContext) : base(catalogContext)
    {
    }

    protected override Task<Domain.Entities.TrainingAggregate.Training> GetTrainingAccordinglyToCommandAsync(CreateTrainingCommand command)
    {
        var training = new Domain.Entities.TrainingAggregate.Training(command.Title!);
        return Task.FromResult(training);
    }

    protected override CreatedTrainingDto MakeResult(Domain.Entities.TrainingAggregate.Training training)
    {
        return new CreatedTrainingDto()
        {
            Id = training.Id
        };
    }
}
