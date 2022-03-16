using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertUnit
    {
        /// <summary>
        /// 
        /// </summary>

        public static readonly string[] UnitSize = { "Byte", "Kib", "Mib", "Gib", "Tib" };
        /// <summary>
        /// 
        /// </summary>
        public static readonly string[] UnitSizeSpeed = { "Byte/s", "Kib/s", "Mib/s", "Gib/s", "Tib/s" };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="round"></param>
        /// <param name="units"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        public static string ConvertSize(double num, int round, string[] units, int div = 1024)
        {
            if (num == 0) return "0 " + units[0];
            for (double i = 0; i < units.Length; i++)
            {
                double sizeitem = num / Math.Pow(div, i);
                if (sizeitem < 1 && sizeitem > -1)
                {
                    if (i == 0) return "0 " + units[0];
                    else return Math.Round((num / Math.Pow(div, i - 1)), round).ToString() + " " + units[(int)i - 1];
                }
            }
            return Math.Round(num / Math.Pow(div, units.Length - 1), round).ToString() + " " + units[units.Length - 1];
        }
    }
}
