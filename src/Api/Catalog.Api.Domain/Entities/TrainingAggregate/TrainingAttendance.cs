using Ardalis.GuardClauses;
using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Domain.Entities.TrainingAggregate;

public class TrainingAttendance
{
    public int TrainingId { get; init; }

    public virtual Training? Training { get; private set; }

    public Attendance Attendance { get; } = null!;

    private TrainingAttendance()
    {
    }

    public TrainingAttendance(Training? training, Attendance attendance)
    {
        Training   = Guard.Against.Null(training, nameof(training));
        Attendance = Guard.Against.Null(attendance, nameof(attendance));
    }

    protected bool Equals(TrainingAttendance other)
    {
        return TrainingId == other.TrainingId && Attendance.Equals(other.Attendance);
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

        return Equals((TrainingAttendance) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrainingId, Attendance);
    }
}
