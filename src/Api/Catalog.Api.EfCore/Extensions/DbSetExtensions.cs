using System;
using System.Linq.Expressions;
using Catalog.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.EfCore.Extensions;

public static class DbSetExtensions
{
    /// <summary>
    /// Retrieves an <see cref="IEntity" /> by its id.
    /// The tracking system of Entity Framework Core can be disabled.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to be retrieved.</typeparam>
    /// <param name="entityDbSet">The <see cref="DbSet{TEntity}" /> on which will be performed the operation.</param>
    /// <param name="id">The Id of the entity to look for.</param>
    /// <param name="disableTracking"><see langword="bool" /> to indicate if the change tracker should track or not the found entity.</param>
    /// <param name="cancellationToken">An optional token to cancel the operation. Default value is <see cref="CancellationToken.None" />.</param>
    /// <returns> A task representing the asynchronous operations. The task result is a <see cref="Nullable{T}" /> with T being an <see cref="IEntity" />.</returns>
    public static async Task<TEntity?> GetByIdAsync<TEntity>(this DbSet<TEntity> entityDbSet, Guid id, bool disableTracking = false, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        if (disableTracking)
        {
            return await entityDbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        return await entityDbSet.FindAsync(id);
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(this DbSet<TEntity> entityDbSet,
        Guid id,
        Expression<Func<TEntity, bool>> wherePredicate,
        bool disableTracking = false,
        CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        var query = entityDbSet.Where(wherePredicate);
        if (disableTracking)
        {
            return await query.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        }

        return await query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves every entities of a given <see cref="DbSet{TEntity}" />.
    /// An option where clause statement can be provide to apply filtering.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities that will be returned.</typeparam>
    /// <param name="entityDbSet">The source <see cref="DbSet{TEntity}"/> o which will be performed the operation.</param>
    /// <param name="wherePredicate">An optional <see langword="Func{TEntity, bool}"/> to apply filtering.</param>
    /// <param name="disableTracking">Option <see langword="bool" /> to disable Entity Framework Core's tracking. Default value is <see langword="true" />.</param>
    /// <param name="cancellationToken">An optional token to cancel the operation. Default value is <see cref="CancellationToken.None" />.</param>
    /// <returns>A collection of <see cref="IEntity" />.</returns>
    public static Task<List<TEntity>> GetAllAsync<TEntity>(this DbSet<TEntity> entityDbSet, Expression<Func<TEntity, bool>>? wherePredicate = default, bool disableTracking = default, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> query = entityDbSet;
        if (wherePredicate is not null)
        {
            query = query.Where(wherePredicate);
        }

        return disableTracking ? query.AsNoTracking().ToListAsync(cancellationToken) : query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Checks by an id if a given entity exists.
    /// </summary>
    /// <typeparam name="TEntity">The type of the <see cref="IEntity" /> that will be searched.</typeparam>
    /// <param name="entityDbSet">The <see cref="DbSet{TEntity}" /> under which the operation will be performed.</param>
    /// <param name="id">The id of the <see cref="IEntity" /> to look for.</param>
    /// <param name="cancellationToken">An optional token to cancel the operation. Default value is <see cref="CancellationToken.None" />.</param>
    /// <returns>True if the entity exists false otherwise.</returns>
    public static async Task<bool> ExistsAsync<TEntity>(this DbSet<TEntity> entityDbSet, Guid id, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return await entityDbSet.AnyAsync(entity => entity.Id == id, cancellationToken);
    }

    /// <summary>
    /// Checks every a set of given id's entities exists.
    /// </summary>
    /// <typeparam name="TEntity">The type of the <see cref="IEntity" /> that will be searched.</typeparam>
    /// <param name="entityDbSet">The <see cref="DbSet{TEntity}" /> under which the operation will be performed.</param>
    /// <param name="ids">The set of ids that should exist.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if every supplied <paramref name="ids" /> exist, false otherwise.</returns>
    public static async Task<bool> ExistsAsync<TEntity>(this DbSet<TEntity> entityDbSet, IEnumerable<Guid> ids, CancellationToken cancellationToken = default) where TEntity : class, IEntity
    {
        return await entityDbSet.CountAsync(entity => ids.Contains(entity.Id), cancellationToken) == ids.Count();
    }
}
