using Ardalis.GuardClauses;
using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingAudience
{
    public int TrainingId { get; }

    public Training? Training { get; private set; }

    public Audience Audience { get; private set; } = null!;

    private TrainingAudience()
    {

    }

    public TrainingAudience(Training training, Audience audience) : this()
    {
        Guard.Against.Null(training, nameof(training));
        Guard.Against.Null(audience, nameof(audience));
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
