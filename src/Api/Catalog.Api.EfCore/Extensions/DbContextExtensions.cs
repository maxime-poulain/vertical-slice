using Catalog.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.EfCore.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    /// Inserts into the database a given <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to insert into the database.</typeparam>
    /// <param name="dbContext">Underlying <see cref="DbContext" /> that will perform the operation.</param>
    /// <param name="entity"><see cref="IEntity" /> to insert into the database.</param>
    /// <param name="cancellationToken">Optional token to cancel the inserting operation.</param>
    /// <returns>A task that represents the asynchronous operations. The task's result is the inserted entity.</returns>
    public static async Task<TEntity> InsertAsync<TEntity>(this DbContext dbContext, TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        var entitySet = dbContext.Set<TEntity>();
        entitySet.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}