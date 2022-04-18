using System.Text.Json;
using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Extensions;
using Catalog.Api.Domain.CQS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Application.MediatR.Behaviors.Logging;

public class QueryLoggingBehavior<TQuery, TResponse> : IPipelineBehavior<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    private readonly Guid _queryId = Guid.NewGuid();
    private readonly ILogger<QueryLoggingBehavior<TQuery, TResponse>> _logger;

    public QueryLoggingBehavior(ILogger<QueryLoggingBehavior<TQuery, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Processing query `{query}`. {id}", query.GetGenericTypeName(), _queryId);
        try
        {
            return await next();
        }
        catch (Exception exception) when (exception is not EntityNotFoundException)
        {
            _logger.LogError(exception, "An error occurred while executing query `{id}`. {serializedQuery}", _queryId, JsonSerializer.Serialize(query));
            throw new LoggedMediatRException();
        }
    }
}
