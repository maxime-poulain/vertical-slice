namespace Catalog.Api.Application.Features.Training.Edit;

public record EditedTrainingDto
{
    public int TrainingId { get; init; }

    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Goal { get; init; } = null!;

    public int TrainingTypeId { get; init; }

    public string? TrainingTypeDescription { get; init; }
}