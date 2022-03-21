using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.MediatR.Behaviors;

/// <summary>
/// Disables tracking of entity framework during the execution of queries.
/// </summary>
/// <typeparam name="TRequest">Type of the <see cref="IQuery" />'s current execution.</typeparam>
/// <typeparam name="TResponse"><typeparamref name="TRequest" />'s response type.</typeparam>
public class NoEfTrackingDuringQueryPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IQuery<TResponse>
{
    private readonly CatalogContext _catalogContext;

    public NoEfTrackingDuringQueryPipelineBehavior(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var previousTrackingBehavior = _catalogContext.ChangeTracker.QueryTrackingBehavior;
        try
        {
            _catalogContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await next();
        }
        finally
        {
            _catalogContext.ChangeTracker.QueryTrackingBehavior = previousTrackingBehavior;
        }
    }
}