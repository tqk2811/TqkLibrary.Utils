using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetName(this Type type, bool isFullName = false)
        {
            if (type.IsGenericType)
            {
                string[] names = type.GenericTypeArguments.Select(x => GetName(x, isFullName)).ToArray();
                return $"{type.Name.Replace("`1", string.Empty)}<{string.Join(", ", names)}>";
            }
            else
            {
                if (isFullName) return type.FullName!;
                return type.Name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetNameFixed(this Type type, bool isFullName = false, char lt = '〈', char gt = '〉')
        {
            string name = type.GetName(isFullName);
            name = name.Replace('>', gt).Replace('<', lt);//〈 〉‹ ›《 》
            Path.GetInvalidFileNameChars().ToList().ForEach(c => name = name.Replace(c, '_'));
            return name;
        }

    }
}
