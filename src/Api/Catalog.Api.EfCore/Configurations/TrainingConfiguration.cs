using Catalog.Api.Domain.Entities;
using Catalog.Api.Domain.Enumerations.Training;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations;

public class TrainingConfiguration : EntityConfiguration<Training>
{
    public override void Configure(EntityTypeBuilder<Training> entityTypeBuilder)
    {
        base.Configure(entityTypeBuilder);

        entityTypeBuilder.Property(training => training.Title)
            .HasMaxLength(100)
            .IsRequired();

        entityTypeBuilder.Property(training => training.Description)
            .HasMaxLength(500)
            .IsRequired();

        entityTypeBuilder.Property(training => training.Goal)
            .HasMaxLength(500)
            .IsRequired();

        entityTypeBuilder
            .Property(training => training.TrainingType)
            .HasConversion(trainingType => trainingType.Value,
                value => TrainingType.FromValue(value));
    }
}