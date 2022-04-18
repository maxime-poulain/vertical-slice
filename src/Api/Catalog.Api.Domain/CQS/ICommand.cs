using MediatR;

namespace Catalog.Api.Domain.CQS;

public interface ICommand
{
}

public interface ICommand<out TResponse> : ICommand, IRequest<TResponse>
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}
