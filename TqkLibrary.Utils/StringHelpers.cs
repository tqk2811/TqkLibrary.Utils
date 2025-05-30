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
    }
}
