using System;
using System.Collections.Generic;
using System.Text;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class RandomNext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static TimeSpan Next(this Random random, TimeSpan min, TimeSpan max)
        {
            if (min == max) return min;
            if (max < min) throw new ArgumentException("max must large than min");

            double diff = (max - min).TotalSeconds;
            return min + TimeSpan.FromSeconds(random.NextDouble() * diff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static DateTime Next(this Random random, DateTime min, DateTime max)
        {
            if (min == max) return min;
            if (max < min) throw new ArgumentException("max must large than min");

            double diff = (max - min).TotalSeconds;
            return min + TimeSpan.FromSeconds(random.NextDouble() * diff);
        }
    }
}
