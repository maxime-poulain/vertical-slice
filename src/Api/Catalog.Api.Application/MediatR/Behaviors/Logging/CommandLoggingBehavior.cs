using System.Text.Json;
using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Extensions;
using Catalog.Api.Domain.CQS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Application.MediatR.Behaviors.Logging;

public class CommandLoggingBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    private readonly Guid _commandId = Guid.NewGuid();
    private readonly ILogger<IPipelineBehavior<TCommand, TResponse>> _logger;

    public CommandLoggingBehavior(ILogger<IPipelineBehavior<TCommand, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Processing command `{command}`. TraceId `{id}`", command.GetGenericTypeName(), _commandId);
        try
        {
            return await next();
        }
        catch (Exception exception) when (exception is not EntityNotFoundException)
        {
            _logger.LogError(exception, "An error occurred while executing command `{id}`. {serializedQuery}", _commandId, JsonSerializer.Serialize(command));
            throw new LoggedMediatRException();
        }
    }
}