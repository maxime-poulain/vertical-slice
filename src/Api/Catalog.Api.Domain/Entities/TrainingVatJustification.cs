using Ardalis.GuardClauses;
using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingVatJustification
{
    public int TrainingId { get; private set; }

    public Training? Training { get; private set; }

    public VatJustification VatJustification { get; private set; } = null!;

    private TrainingVatJustification()
    {

    }

    public TrainingVatJustification(Training? training, VatJustification? vatJustification) : this()
    {
        Guard.Against.Null(training, nameof(training));
        Guard.Against.Null(vatJustification, nameof(vatJustification));
        Training         = training;
        VatJustification = vatJustification;
    }

    protected bool Equals(TrainingVatJustification other)
    {
        return TrainingId == other.TrainingId && VatJustification.Equals(other.VatJustification);
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

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((TrainingVatJustification) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrainingId, VatJustification);
    }
}
