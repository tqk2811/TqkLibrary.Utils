using Nito.AsyncEx;
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
            T result = await task.ConfigureAwait(false);
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            return result;
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
            await task;
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskFactory"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task StartNewWithAsyncContext(this TaskFactory taskFactory, Action action)
        {
            return taskFactory.StartNew(
                () => { AsyncContext.Run(action); },
                TaskCreationOptions.LongRunning
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskFactory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<T> StartNewWithAsyncContext<T>(this TaskFactory taskFactory, Func<T> func)
        {
            return taskFactory.StartNew<T>(
                () => { return AsyncContext.Run(func); },
                TaskCreationOptions.LongRunning
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskFactory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task StartNewWithAsyncContext(this TaskFactory taskFactory, Func<Task> func)
        {
            return taskFactory.StartNew(
                () => { AsyncContext.Run(async () => await func()); },
                TaskCreationOptions.LongRunning
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="taskFactory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<T> StartNewWithAsyncContext<T>(this TaskFactory taskFactory, Func<Task<T>> func)
        {
            return taskFactory.StartNew<T>(
                () => { return AsyncContext.Run(async () => await func()); },
                TaskCreationOptions.LongRunning
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks) => Task.WhenAll(tasks);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static Task WhenAll<T>(this IEnumerable<Task> tasks) => Task.WhenAll(tasks);


#if DEBUG
        static async void Test()
        {
            //fix async () =>
            await Task.Factory.StartNewWithAsyncContext(async () => { });
            int a = await Task.Factory.StartNewWithAsyncContext(async () => { return 2; });
        }
#endif
    }
}
