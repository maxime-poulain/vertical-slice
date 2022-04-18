using Catalog.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations;

public class TrainingAssignmentConfiguration : IEntityTypeConfiguration<TrainingAssignment>
{
    public void Configure(EntityTypeBuilder<TrainingAssignment> builder)
    {
        builder.HasKey(entity => new {entity.TrainingId, entity.TrainerId});

        builder.HasOne(trainingAssignment => trainingAssignment.Training)
            .WithMany(training => training.Assignments)
            .HasForeignKey(trainingAssignment => trainingAssignment.TrainingId);

        builder.HasOne(trainingAssignment => trainingAssignment.Trainer)
            .WithMany(trainer => trainer.TrainingAssignments)
            .HasForeignKey(trainingAssignment => trainingAssignment.TrainerId);

        builder.HasIndex(p => new { p.TrainingId });

        builder.HasIndex(p => new { p.TrainerId });
    }
}
