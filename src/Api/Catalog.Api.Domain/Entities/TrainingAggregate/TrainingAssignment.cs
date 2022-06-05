using Catalog.Api.Domain.Entities.TrainerAggregate;

namespace Catalog.Api.Domain.Entities.TrainingAggregate;

public class TrainingAssignment
{
    public Guid TrainerId { get; init; }

    public Guid TrainingId { get; init; }

    public virtual Trainer? Trainer { get; init; }

    public virtual Training? Training { get; init; }

    public TrainingAssignment()
    {
    }
}
