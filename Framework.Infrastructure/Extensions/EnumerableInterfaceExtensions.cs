using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableInterfaceExtensions
    {
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (source.IsNullOrEmpty())
            {
                return source;
            }

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || !source.Any())
            {
                return true;
            }

            return false;
        }
    }
}
