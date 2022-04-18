using MediatR;

namespace Catalog.Api.Domain.CQS;

public interface IQuery
{
}

public interface IQuery<out TResponse> : IQuery, IRequest<TResponse>
{
}

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
}
