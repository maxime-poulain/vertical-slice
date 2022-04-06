using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Application.Features.Training.GetTrainingForAdminPageListByTrainerId;

public record TrainingForAdminPageDto : TrainingDto
{
    public IEnumerable<Topic>? Topics { get; set; }
}
