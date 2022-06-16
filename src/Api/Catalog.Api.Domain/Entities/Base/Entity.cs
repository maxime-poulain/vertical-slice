namespace Catalog.Api.Domain.Entities.Base;

public class Entity : IEntity
{
    public Guid Id { get; protected set; }

    public List<IDomainEvent> DomainEvents { get; } = new();

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public DateTime? DeletedOn { get; set; }
}

public interface IEntity
{
    public Guid Id { get; }

    public List<IDomainEvent> DomainEvents { get; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public DateTime? DeletedOn { get; set; }
}
