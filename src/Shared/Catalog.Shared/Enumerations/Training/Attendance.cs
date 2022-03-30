using Ardalis.SmartEnum;

namespace Catalog.Shared.Enumerations.Training;

public class Attendance : SmartEnum<Attendance>
{
    public static readonly Attendance Individual = new (nameof(Individual), 1);
    public static readonly Attendance Collective = new (nameof(Collective), 2);

    public Attendance(string name, int value) : base(name, value)
    {
    }
}