using Catalog.Api.Domain.Entities.Base;

namespace Catalog.Api.Application.Common.Exceptions;

/// <summary>
/// <see cref="Exception" /> thrown when attempting to perform an operation on an non existing <see cref="IEntity" />.
/// </summary>
public class EntityNotFoundException : Exception
{
    public Guid EntityId { get; }

    public Type EntityType { get; } = null!;

    public EntityNotFoundException(Guid entityId, Type entityType) : base(ErrorMessage(entityId, entityType))
    {
        EntityId   = entityId;
        EntityType = entityType;
    }

    private static string ErrorMessage(Guid entityId, Type entityType)
    {
        return $"Entity {entityType.FullName ?? string.Empty} with id {entityId} not found";
    }

    private EntityNotFoundException(string? message) : base(message)
    {
    }

    protected EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
