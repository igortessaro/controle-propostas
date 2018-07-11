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

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
}
