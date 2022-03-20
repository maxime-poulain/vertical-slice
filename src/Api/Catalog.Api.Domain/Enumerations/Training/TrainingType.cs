using Ardalis.SmartEnum;

namespace Catalog.Api.Domain.Enumerations.Training;

public class TrainingType : SmartEnum<TrainingType>
{
    public static readonly TrainingType Individual  = new(nameof(Individual), 1);
    public static readonly TrainingType Group       = new(nameof(Group), 2);

    protected TrainingType(string name, int value) : base(name, value)
    {
    }
}