using Catalog.Api.Domain.Entities;
using Catalog.Api.Domain.Entities.TrainerAggregate;
using Catalog.Api.Domain.Entities.TrainerAggregate.Messages;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.EfCore.Extensions;

public static class SeedingOperationExtensions
{
    public static async Task SeedDefaultTrainerAsync(this CatalogContext catalogContext)
    {
        if (!await catalogContext.Trainer.ExistsAsync(1))
        {
            var trainer = Default();

            // We need to make sure the id of the trainer is one.
            catalogContext.Entry(trainer).Property(entity => entity.Id).CurrentValue = 1;

            await catalogContext.InsertAsync(trainer);
        }

        static Trainer Default() => Trainer.Create(new CreateTrainerMessage("Toto",
            "Lechacal",
            "Professional tinker",
            "Dans de nombreuses histoires le mettant en scène, Toto est présenté sous de nombreux traits de caractères, notamment comme un mauvais garnement, un cancre impertinent, etc. Le décor de ses farces est souvent celui de l'école ou de sa maison. Plusieurs auteurs et dessinateurs ont imaginé un visage et un univers à ce personnage, en lui donnant une famille, des amis, une maîtresse…", "toto@lechacal.com",
            null));
    }
}
