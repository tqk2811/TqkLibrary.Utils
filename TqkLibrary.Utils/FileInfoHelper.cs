using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileInfoHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetFileNameWithoutExtension(this FileInfo fileInfo)
        {
            if (fileInfo is null) throw new ArgumentNullException(nameof(fileInfo));
            if (fileInfo.Extension.Length == 0) return fileInfo.Name;
            return fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
        }
    }
}
