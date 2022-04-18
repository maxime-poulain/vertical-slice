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
}
