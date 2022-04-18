using MediatR;

namespace Catalog.Api.Domain.Entities.Base;

public interface IDomainEvent : INotification
{
}

public interface  IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
}
