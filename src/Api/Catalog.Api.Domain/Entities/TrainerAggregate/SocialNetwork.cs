using Catalog.Shared.Enumerations.Trainer;

namespace Catalog.Api.Domain.Entities.TrainerAggregate;

public class TrainerSocialNetwork
{
    public int TrainerId { get; private set; }

    public SocialNetwork SocialNetwork { get; private set; } = null!;

    public string? Url { get; private set; }

    public virtual Trainer? Trainer { get; private set; }

    private TrainerSocialNetwork()
    {
    }

    public TrainerSocialNetwork(Trainer trainer, SocialNetwork socialNetwork, string? url)
    {
        TrainerId     = trainer.Id;
        Trainer       = trainer;
        SocialNetwork = socialNetwork;
        Url           = url;
    }

    protected bool Equals(TrainerSocialNetwork other)
    {
        return TrainerId == other.TrainerId && SocialNetwork.Equals(other.SocialNetwork) && Url == other.Url;
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

        return Equals((TrainerSocialNetwork)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrainerId, SocialNetwork, Url);
    }
}
