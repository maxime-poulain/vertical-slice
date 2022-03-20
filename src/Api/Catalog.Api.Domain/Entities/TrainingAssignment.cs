namespace Catalog.Api.Domain.Entities;

public class TrainingAssignment
{
    public int TrainerId { get; init; }

    public int TrainingId { get; init; }

    public virtual Trainer? Trainer { get; init; }

    public virtual Training? Training { get; init; }

    public TrainingAssignment ()
    {

    }
}