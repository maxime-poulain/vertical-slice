using Ardalis.SmartEnum;

namespace Catalog.Shared.Enumerations.Training;

public class Audience : SmartEnum<Audience>
{
    public static readonly Audience OutOfWork = new(nameof(OutOfWork), 1);
    public static readonly Audience Employee  = new(nameof(Employee), 2);
    public static readonly Audience Student   = new(nameof(Student)  , 3);

    public Audience(string name, int value) : base(name, value)
    {
    }
}
