using System;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TimeSpanHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static TimeSpan GetTimeInDay(this TimeSpan timeSpan)
        {
            while (timeSpan.TotalDays >= 1.0)
            {
                timeSpan -= TimeSpan.FromDays(1);
            }
            return timeSpan;
        }
    }
}
