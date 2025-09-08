using System;
using System.Collections.Generic;
using System.Text;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Enum Or(this Enum a, Enum b)
        {
            if (Enum.GetUnderlyingType(a.GetType()) != typeof(ulong))
            {
                return (Enum)Enum.ToObject(a.GetType(), Convert.ToInt64(a) | Convert.ToInt64(b));
            }

            return (Enum)Enum.ToObject(a.GetType(), Convert.ToUInt64(a) | Convert.ToUInt64(b));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Enum And(this Enum a, Enum b)
        {
            if (Enum.GetUnderlyingType(a.GetType()) != typeof(ulong))
            {
                return (Enum)Enum.ToObject(a.GetType(), Convert.ToInt64(a) & Convert.ToInt64(b));
            }

            return (Enum)Enum.ToObject(a.GetType(), Convert.ToUInt64(a) & Convert.ToUInt64(b));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Enum Not(this Enum a)
        {
            return (Enum)Enum.ToObject(a.GetType(), ~Convert.ToInt64(a));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static T RemoveFlag<T>(this T @enum, T flag) where T : struct, Enum
        {
            return (T)@enum.And(flag.Not());
        }
    }
}
