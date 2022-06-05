using Catalog.Api.Domain.Entities.TrainerAggregate;
using Catalog.Shared.Enumerations.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations.TrainerAggregate;

public class TrainerSocialNetworkConfiguration : IEntityTypeConfiguration<TrainerSocialNetwork>
{
    public void Configure(EntityTypeBuilder<TrainerSocialNetwork> builder)
    {
        builder.HasKey(socialNetwork => new { socialNetwork.TrainerId, socialNetwork.SocialNetwork });

        builder.Property(socialNetwork => socialNetwork.SocialNetwork)
            .HasConversion(e => e.Value,
                value => SocialNetwork.FromValue(value));

        builder.HasOne(trainerSocialNetwork => trainerSocialNetwork.Trainer)
            .WithMany(trainer => trainer.SocialNetworks)
            .HasForeignKey(socialNetwork => socialNetwork.TrainerId);
    }
}
