using Ardalis.SmartEnum;

namespace Catalog.Shared.Enumerations.Training;

public class VatJustification : SmartEnum<VatJustification>
{
    public static readonly VatJustification LanguageCourse  = new(nameof(LanguageCourse), 1);
    public static readonly VatJustification Professional    = new(nameof(Professional), 2);
    public static readonly VatJustification ScholarTraining = new(nameof(ScholarTraining), 3);

    private VatJustification(string name, int value) : base(name, value)
    {
    }
}
