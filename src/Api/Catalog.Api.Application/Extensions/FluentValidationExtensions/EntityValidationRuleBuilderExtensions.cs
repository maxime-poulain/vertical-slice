using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Extensions.FluentValidationExtensions;

public static class EntityValidationRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, int> EntityExistsAsync<T, TEntity>(this IRuleBuilder<T,int> ruleBuilder, CatalogContext context) where TEntity : class, IEntity
    {
        return ruleBuilder.MustAsync((entityId, token) => context.Set<TEntity>().ExistsAsync(entityId, token));
    }

    public static IRuleBuilderOptionsConditions<T, int> ThrowErrorIfEntityDoesNotExistAsync<T, TEntity>(this IRuleBuilder<T, int> ruleBuilder, DbContext dbContext, CancellationToken cancellationToken = default)
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

    public static IRuleBuilderOptionsConditions<T, int> ThrowErrorIfEntityDoesNotExistAsync<T, TEntity>(this IRuleBuilder<T, int> ruleBuilder, DbSet<TEntity> set, CancellationToken cancellationToken = default)
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