using Ardalis.SmartEnum;

namespace Catalog.Api.Domain.Enumerations.Trainer;

public class TrainerSkillLevel : SmartEnum<TrainerSkillLevel>
{
    public static readonly TrainerSkillLevel Beginner     = new(nameof(Beginner), 1);
    public static readonly TrainerSkillLevel Intermediate = new(nameof(Intermediate), 2);
    public static readonly TrainerSkillLevel Advanced     = new(nameof(Advanced), 3);

    protected TrainerSkillLevel(string name, int value) : base(name, value)
    {
    }
}