using System;
using System.Collections.Generic;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string Join(this IEnumerable<string> enumerable, string separator)
        {
            if (string.IsNullOrWhiteSpace(separator)) throw new ArgumentNullException(nameof(separator));
            if (enumerable is null) throw new ArgumentNullException(nameof(enumerable));
            return string.Join(separator, enumerable);
        }

#if NETSTANDARD2_0
        //.NET: Core 2.1, Core 2.2, Core 3.0, Core 3.1, 5, 6, 7, 8, 9, 10
        //NET Standard: 2.1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string text, string value, StringComparison stringComparison = default)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string text, char value, StringComparison stringComparison = default)
            => text.Contains(value.ToString(), stringComparison);
#endif

    }
}
