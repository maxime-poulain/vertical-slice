using Ardalis.SmartEnum;

namespace Catalog.Shared.Enumerations.Trainer;
public class SocialNetwork : SmartEnum<SocialNetwork>
{
    public static readonly SocialNetwork Facebook        = new(nameof(Facebook), 1);
    public static readonly SocialNetwork Twitter         = new(nameof(Twitter), 2);
    public static readonly SocialNetwork Instagram       = new(nameof(Instagram), 3);
    public static readonly SocialNetwork LinkedIn        = new(nameof(LinkedIn), 4);
    public static readonly SocialNetwork PersonalWebsite = new(nameof(PersonalWebsite), 5);

    protected SocialNetwork(string name, int value) : base(name, value)
    {
    }
}
