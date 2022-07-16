using System.Collections.Generic;

namespace Tfl.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> value)
        {
            return value ?? new List<T>();
        }
    }
}
