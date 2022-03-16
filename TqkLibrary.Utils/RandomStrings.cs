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
    public static class RandomStrings
    {
        private static Random rd = new Random();
        private const string chars_lower = "abcdefghijklmnopqrstuvwxyz";
        private const string chars_upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string charsAndNum = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string Nums = "0123456789";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        /// <returns></returns>
        public static string RandomString(int lengthMin, int lengthMax)
        {
            return new string(Enumerable.Repeat(chars, rd.Next(lengthMin, lengthMax + 1)).Select(s => s[rd.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        /// <returns></returns>
        public static string RandomNumber(int lengthMin, int lengthMax)
        {
            return new string(Enumerable.Repeat(Nums, rd.Next(lengthMin, lengthMax + 1)).Select(s => s[rd.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        /// <returns></returns>
        public static string RandomStringAndNum(int lengthMin, int lengthMax)
        {
            return new string(Enumerable.Repeat(charsAndNum, rd.Next(lengthMin, lengthMax + 1)).Select(s => s[rd.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        /// <returns></returns>
        public static string RandomStringUpper(int lengthMin, int lengthMax)
        {
            return new string(Enumerable.Repeat(chars_upper, rd.Next(lengthMin, lengthMax + 1)).Select(s => s[rd.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lengthMin"></param>
        /// <param name="lengthMax"></param>
        /// <returns></returns>
        public static string RandomStringLower(int lengthMin, int lengthMax)
        {
            return new string(Enumerable.Repeat(chars_lower, rd.Next(lengthMin, lengthMax + 1)).Select(s => s[rd.Next(s.Length)]).ToArray());
        }
    }
}
