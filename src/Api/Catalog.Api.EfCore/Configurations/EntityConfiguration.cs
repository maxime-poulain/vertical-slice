using Catalog.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.EfCore.Configurations;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

        builder.Ignore(entity => entity.DomainEvents);

        builder.Property(e => e.CreatedOn)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(e => e.ModifiedOn)
            .HasPrecision(2);

        builder.Property(e => e.DeletedOn)
            .HasPrecision(2);
    }
}