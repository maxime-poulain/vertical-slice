namespace Catalog.Api.Application.Features.Trainer.CreateTrainer;

public record TrainerCreationResult
{
    public Guid TrainerId { get; init; }
}
