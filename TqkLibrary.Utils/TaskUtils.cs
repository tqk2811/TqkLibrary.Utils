using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> DelayAsync<T>(this Task<T> task, int delay, CancellationToken cancellationToken = default)
        {
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            return await task;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="delay"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task DelayAsync(this Task task, int delay, CancellationToken cancellationToken = default)
        {
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            await task;
        }
    }
}
