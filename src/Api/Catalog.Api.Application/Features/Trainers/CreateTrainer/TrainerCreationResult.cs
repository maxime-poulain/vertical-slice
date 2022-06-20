namespace Catalog.Api.Application.Features.Trainers.CreateTrainer;

public record TrainerCreationResult
{
    public Guid TrainerId { get; init; }
}
