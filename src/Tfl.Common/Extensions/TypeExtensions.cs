using System;
using System.Linq;

namespace Tfl.Common.Extensions
{
    /// <summary>
    /// Extension methods for Type type.
    /// </summary>
    public static class TypeExtensions
    {
        public static bool ImplementsInterface(this Type type, Type @interface)
        {
            if (type == null || @interface == null)
            {
                return false;
            }

            return @interface.GenericTypeArguments.Length > 0
                ? @interface.IsAssignableFrom(type)
                : type.GetInterfaces().Any(c => string.Equals(c.Name, @interface.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
