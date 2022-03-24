using Catalog.Api.Domain.Enumerations.Training;

namespace Catalog.Api.Domain.Entities;

public class TrainingTopic
{
    public int TrainingId { get; init; }

    public Training? Training { get; set; }

    public Topic Topic { get; init; } = null!;

    private TrainingTopic()
    {

    }

    public TrainingTopic(Training training, Topic topic) : this()
    {
        Training   = training;
        TrainingId = Training.Id;
        Topic      = topic;
    }

    public override bool Equals(object? obj)
    {
        if (obj is TrainingTopic trainingTopic)
        {
            return Equals(trainingTopic);
        }

        return false;
    }

    protected bool Equals(TrainingTopic other)
    {
        return TrainingId == other.TrainingId && Topic == other.Topic;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TrainingId, Topic);
    }
}