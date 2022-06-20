using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Application.Features.Trainings.GetAll;

public class TrainingDto
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }

    public IEnumerable<Topic>? Topics { get; set; }
}
