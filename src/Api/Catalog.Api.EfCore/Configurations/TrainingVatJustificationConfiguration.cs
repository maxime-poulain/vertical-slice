using Catalog.Api.Domain.Entities;
using Catalog.Api.Domain.Entities.TrainingAggregate;
using Catalog.Shared.Enumerations.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations;

public class TrainingVatJustificationConfiguration : IEntityTypeConfiguration<TrainingVatJustification>
{
    public void Configure(EntityTypeBuilder<TrainingVatJustification> builder)
    {
        builder.HasKey(attendance => new { attendance.VatJustification, attendance.TrainingId });

        builder.Property(trainingTopic => trainingTopic.VatJustification).HasConversion
        (
            attendance => attendance.Value,
            value => VatJustification.FromValue(value)
        );

        builder.HasOne(trainingVatJustification => trainingVatJustification.Training)
            .WithMany(training => training.VatJustifications)
            .HasForeignKey(trainingVatJustification => trainingVatJustification.TrainingId);

        builder.HasIndex(trainingVatJustification => trainingVatJustification.TrainingId);
        builder.HasIndex(trainingVatJustification => trainingVatJustification.VatJustification);
    }
}
