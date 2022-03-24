using Catalog.Api.Domain.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingAudience
{
    public int TrainingId { get; }

    public Training Training { get; init; }

    public Audience Audience { get; init; } = null!;

    private TrainingAudience()
    {

    }

    public TrainingAudience(Training training, Audience audience) : this()
    {
        Training   = training;
        TrainingId = training.Id;
        Audience   = audience;
    }

    protected bool Equals(TrainingAudience other)
    {
        return TrainingId == other.TrainingId && Audience.Equals(other.Audience);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((TrainingAudience) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrainingId, Audience);
    }
}