using System.Collections.Generic;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> collection, TSource item)
        {
            foreach (var entry in collection)
                if (!entry.Equals(item))
                    yield return entry;
        }
    }
}
