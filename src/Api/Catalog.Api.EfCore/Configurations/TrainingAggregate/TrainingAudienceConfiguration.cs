using Catalog.Api.Domain.Entities.TrainingAggregate;
using Catalog.Shared.Enumerations.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations.TrainingAggregate;

public class TrainingAudienceConfiguration : IEntityTypeConfiguration<TrainingAudience>
{
    public void Configure(EntityTypeBuilder<TrainingAudience> builder)
    {
        builder.HasKey(attendance => new { attendance.Audience, attendance.TrainingId });

        builder.Property(trainingTopic => trainingTopic.Audience).HasConversion
        (
            attendance => attendance.Value,
            value => Audience.FromValue(value)
        );

        builder.HasOne(trainingAudience => trainingAudience.Training)
            .WithMany(training => training.Audiences)
            .HasForeignKey(trainingAudience => trainingAudience.TrainingId);

        builder.HasIndex(trainingAudience => trainingAudience.TrainingId);
        builder.HasIndex(trainingAudience => trainingAudience.Audience);
    }
}
