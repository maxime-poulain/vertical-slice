namespace Catalog.Api.Domain.Extensions;

public static class CollectionExtensions
{
    public static ICollection<T> RemoveAll<T>(this ICollection<T> sources, IEnumerable<T> itemsToRemove)
    {
        foreach (var itemToRemove in itemsToRemove)
        {
            sources.Remove(itemToRemove);
        }

        return sources;
    }

    /// <summary>
    /// Replaces the content of a collection of entities with a new set of items.
    /// </summary>
    /// <typeparam name="TEntity">The entity type of the source collection.</typeparam>
    /// <typeparam name="T">An arbitrary type that will be converted to <typeparamref name="TEntity"/> with <paramref name="elementToEntityConverter"/>.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="elements">The elements after conversion that will be part of source.</param>
    /// <param name="elementToEntityConverter">A Fun that converts an item of <paramref name="elements"/> to an object of type <typeparamref name="TEntity"/>.</param>
    public static void ReplaceWith<TEntity, T>(this ICollection<TEntity> source, IEnumerable<T>? elements, Func<T, TEntity> elementToEntityConverter)
        where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(source);

        source.Clear();
        if (elements is null)
        {
            return;
        }

        foreach (var entity in elements)
        {
            source.Add(elementToEntityConverter(entity));

        }
    }
}
