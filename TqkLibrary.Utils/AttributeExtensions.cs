using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (string.IsNullOrWhiteSpace(name))
            {
                return Enumerable.Empty<TAttribute>();
            }

            FieldInfo? field = type.GetField(name);
            if (field is null)
            {
                return Enumerable.Empty<TAttribute>();
            }

            return field.GetCustomAttributes(false).OfType<TAttribute>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute? GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
            => value.GetAttributes<TAttribute>().SingleOrDefault();
    }
}
