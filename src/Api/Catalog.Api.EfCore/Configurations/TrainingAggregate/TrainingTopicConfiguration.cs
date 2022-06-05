using Catalog.Api.Domain.Entities.TrainingAggregate;
using Catalog.Shared.Enumerations.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations.TrainingAggregate;

public class TrainingTopicConfiguration : IEntityTypeConfiguration<TrainingTopic>
{
    public void Configure(EntityTypeBuilder<TrainingTopic> builder)
    {
        builder.HasKey(topic => new { topic.Topic, topic.TrainingId });

        builder.Property(trainingTopic => trainingTopic.Topic).HasConversion
        (
            topic => topic.Value,
            value => Topic.FromValue(value)
        );

        builder.HasOne(trainingTopic => trainingTopic.Training)
            .WithMany(training => training.Topics)
            .HasForeignKey(e => e.TrainingId);

        builder.HasIndex(trainingTopic => trainingTopic.TrainingId);
        builder.HasIndex(trainingTopic => trainingTopic.Topic);
    }
}
