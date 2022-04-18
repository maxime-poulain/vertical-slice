using Catalog.Api.Domain.Entities;
using Catalog.Shared.Enumerations.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations;

public class TrainingAttendanceConfiguration : IEntityTypeConfiguration<TrainingAttendance>
{
    public void Configure(EntityTypeBuilder<TrainingAttendance> builder)
    {
        builder.HasKey(attendance => new { attendance.Attendance, attendance.TrainingId });

        builder.Property(trainingTopic => trainingTopic.Attendance).HasConversion
        (
            attendance => attendance.Value,
            value => Attendance.FromValue(value)
        );

        builder.HasOne(trainingAttendance => trainingAttendance.Training)
            .WithMany(training => training.Attendances)
            .HasForeignKey(trainingAttendance => trainingAttendance.TrainingId);

        builder.HasIndex(trainingAttendance => trainingAttendance.TrainingId);
        builder.HasIndex(trainingAttendance => trainingAttendance.Attendance);
    }
}
