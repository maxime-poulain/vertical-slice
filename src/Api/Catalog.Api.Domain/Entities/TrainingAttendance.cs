using Ardalis.GuardClauses;
using Catalog.Api.Domain.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingAttendance : IEqualityComparer<TrainingAttendance>
{
    public int TrainingId { get; init; }

    public virtual Training? Training { get; }

    public Attendance Attendance { get; }

    private TrainingAttendance()
    {

    }

    public TrainingAttendance(Training training, Attendance? attendance) : this()
    {
        Guard.Against.Null(attendance, nameof(attendance));
        Guard.Against.Null(training, nameof(training));
        Training   = training;
        Attendance = attendance;
    }

    public bool Equals(TrainingAttendance x, TrainingAttendance y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.TrainingId == y.TrainingId && x.Attendance.Equals(y.Attendance);
    }

    public int GetHashCode(TrainingAttendance obj)
    {
        return HashCode.Combine(obj.TrainingId, obj.Attendance);
    }
}