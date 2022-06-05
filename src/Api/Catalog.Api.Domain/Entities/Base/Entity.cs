using Ardalis.SmartEnum;
using Catalog.Api.Domain.Extensions;

namespace Catalog.Api.Domain.Entities.Base;

public class Entity : IEntity
{
    public Guid Id { get; protected set; }

    public List<IDomainEvent> DomainEvents { get; } = new();

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public void SetRelational<T, TEntity>(IEnumerable<T>? enumerations, Func<T, TEntity> factory, HashSet<TEntity> set) where T : SmartEnum<T, int> where TEntity : class
    {
        // If the supplied enumeration is null, this means we remove every links.
        // If the enumeration collection is empty, it will result in the deletion of every links.
        enumerations ??= Array.Empty<T>();

        // Determine the relational entities of type TEntity that can be removed and then remove them.
        var suppliedRelationalEntities = enumerations.Select(factory);
        var relationEntitiesToRemove = set.Except(suppliedRelationalEntities);
        set.RemoveAll(relationEntitiesToRemove);

        //  Determine which ones are not present and therefore need to be added
        var toAdd = suppliedRelationalEntities.Except(set);
        foreach (var entity in toAdd)
        {
            set.Add(entity);
        }
    }

    protected bool Equals(Entity other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Entity) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

public interface IEntity
{
    public Guid Id { get; }

    public List<IDomainEvent> DomainEvents { get; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public DateTime? DeletedOn { get; set; }
}
