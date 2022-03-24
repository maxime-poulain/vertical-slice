using System.Collections;
using Ardalis.GuardClauses;
using Catalog.Api.Domain.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingVatJustification : IEqualityComparer
{
    public int TrainingId { get; }

    public Training? Training { get; }

    public VatJustification VatJustification { get; }

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

    public bool Equals(object? x, object? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return true;
        }

        if (x is TrainingVatJustification v1 && y is TrainingVatJustification v2)
        {
            return v1.TrainingId == v2.TrainingId && v1.VatJustification == v2.VatJustification;
        }

        return false;
    }

    public int GetHashCode(object obj)
    {
        return obj.GetHashCode();
    }
}