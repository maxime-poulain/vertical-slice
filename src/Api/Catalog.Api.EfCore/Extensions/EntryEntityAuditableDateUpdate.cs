using Catalog.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catalog.Api.EfCore.Extensions;

public static class EntryEntityAuditableDateUpdate
{
    public static void UpdateEntitiesAuditableDates(this IEnumerable<EntityEntry<IEntity>> entityEntries)
    {
        foreach (var entityEntry in entityEntries)
        {
            entityEntry.UpdateEntityAuditableDates();
        }
    }

    public static void UpdateEntityAuditableDates(this EntityEntry<IEntity> entityEntry)
    {
        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.UpdateAddedEntityAuditableDates();
        }

        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.UpdateModifiedEntityAuditableDates();
        }

        if (entityEntry.State == EntityState.Deleted)
        {
            entityEntry.UpdateDeletedEntityAuditableDates();
        }
    }

    private static void UpdateAddedEntityAuditableDates(this EntityEntry<IEntity> entity)
    {
        entity.Property<DateTime>(nameof(IEntity.CreatedOn)).CurrentValue = DateTime.UtcNow;
        entity.Property<DateTime?>(nameof(IEntity.ModifiedOn)).IsModified  = false;
        entity.Property<DateTime?>(nameof(IEntity.DeletedOn)).IsModified   = false;
    }

    private static void UpdateModifiedEntityAuditableDates(this EntityEntry<IEntity> entity)
    {
        entity.Property<DateTime>(nameof(IEntity.CreatedOn)).IsModified     = false;
        entity.Property<DateTime?>(nameof(IEntity.ModifiedOn)).CurrentValue = DateTime.UtcNow;
        entity.Property<DateTime?>(nameof(IEntity.DeletedOn)).IsModified     = false;
    }

    private static void UpdateDeletedEntityAuditableDates(this EntityEntry<IEntity> entity)
    {
        entity.Property<DateTime>(nameof(IEntity.CreatedOn)).IsModified    = false;
        entity.Property<DateTime?>(nameof(IEntity.ModifiedOn)).IsModified  = false;
        entity.Property<DateTime?>(nameof(IEntity.DeletedOn)).CurrentValue = DateTime.UtcNow;
    }
}