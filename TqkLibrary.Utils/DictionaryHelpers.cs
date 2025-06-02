using System;
using System.Collections.Generic;
using System.Text;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue? TryGetValue<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.TryGetValue(key, out TValue? value))
            {
                return value;
            }
            else
            {
                return default(TValue);
            }
        }
    }
}
