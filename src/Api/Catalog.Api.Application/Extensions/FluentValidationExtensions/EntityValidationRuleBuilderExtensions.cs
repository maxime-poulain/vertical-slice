using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Extensions.FluentValidationExtensions;

public static class EntityValidationRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, Guid> EntityExistsAsync<T, TEntity>(this IRuleBuilder<T, Guid> ruleBuilder, CatalogContext context) where TEntity : class, IEntity
    {
        return ruleBuilder.MustAsync((entityId, token) => context.Set<TEntity>().ExistsAsync(entityId, token));
    }

    public static IRuleBuilderOptionsConditions<T, Guid> ThrowErrorIfEntityDoesNotExistAsync<T, TEntity>(this IRuleBuilder<T, Guid> ruleBuilder, DbContext dbContext, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return ruleBuilder.CustomAsync(async (entityId,  _,  _) =>
        {
            var existingEntity = await dbContext.Set<TEntity>().ExistsAsync(entityId, cancellationToken);

            if (!existingEntity)
            {
                throw new EntityNotFoundException(entityId, typeof(TEntity));
            }
        });
    }

    public static IRuleBuilderOptionsConditions<T, Guid> ThrowErrorIfEntityDoesNotExistAsync<T, TEntity>(this IRuleBuilder<T, Guid> ruleBuilder, DbSet<TEntity> set, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        return ruleBuilder.CustomAsync(async (entityId,  _,  _) =>
        {
            var existingEntity = await set.ExistsAsync(entityId, cancellationToken);

            if (!existingEntity)
            {
                throw new EntityNotFoundException(entityId, typeof(TEntity));
            }
        });
    }
}
