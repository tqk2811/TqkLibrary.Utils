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



        //continue method: void Continue(Task task, TState state)
        public static Task ContinueWith<TState>(this Task task, Action<Task, TState> continuationAction, TState state)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state);
        public static Task ContinueWith<TState>(this Task task, Action<Task, TState> continuationAction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken);
        public static Task ContinueWith<TState>(this Task task, Action<Task, TState> continuationAction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskContinuationOptions);
        public static Task ContinueWith<TState>(this Task task, Action<Task, TState> continuationAction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskScheduler);
        public static Task ContinueWith<TState>(this Task task, Action<Task, TState> continuationAction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler);


        //continue method: void Continue(Task<T> task, TState state)
        public static Task ContinueWith<T, TState>(this Task<T> task, Action<Task<T>, TState> continuationAction, TState state)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state);
        public static Task ContinueWith<T, TState>(this Task<T> task, Action<Task<T>, TState> continuationAction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken);
        public static Task ContinueWith<T, TState>(this Task<T> task, Action<Task<T>, TState> continuationAction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskContinuationOptions);
        public static Task ContinueWith<T, TState>(this Task<T> task, Action<Task<T>, TState> continuationAction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskScheduler);
        public static Task ContinueWith<T, TState>(this Task<T> task, Action<Task<T>, TState> continuationAction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler);


        //continue method: Task Continue(Task task, TState state)
        public static Task ContinueWith<TState>(this Task task, Func<Task, TState, Task> continuationAction, TState state)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state).Unwrap();
        public static Task ContinueWith<TState>(this Task task, Func<Task, TState, Task> continuationAction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken).Unwrap();
        public static Task ContinueWith<TState>(this Task task, Func<Task, TState, Task> continuationAction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskContinuationOptions).Unwrap();
        public static Task ContinueWith<TState>(this Task task, Func<Task, TState, Task> continuationAction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskScheduler).Unwrap();
        public static Task ContinueWith<TState>(this Task task, Func<Task, TState, Task> continuationAction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler).Unwrap();


        //continue method: Task Continue(Task<T> task, TState state)
        public static Task ContinueWith<T, TState>(this Task<T> task, Func<Task<T>, TState, Task> continuationAction, TState state)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state).Unwrap();
        public static Task ContinueWith<T, TState>(this Task<T> task, Func<Task<T>, TState, Task> continuationAction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken).Unwrap();
        public static Task ContinueWith<T, TState>(this Task<T> task, Func<Task<T>, TState, Task> continuationAction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskContinuationOptions).Unwrap();
        public static Task ContinueWith<T, TState>(this Task<T> task, Func<Task<T>, TState, Task> continuationAction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, taskScheduler).Unwrap();
        public static Task ContinueWith<T, TState>(this Task<T> task, Func<Task<T>, TState, Task> continuationAction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationAction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler).Unwrap();


        //continue method: Task<TNewResult> Continue(Task task, TState state)
        public static Task<TNewResult> ContinueWith<TState, TNewResult>(this Task task, Func<Task, TState, Task<TNewResult>> continuationFunction, TState state)
            => task.ContinueWith((Task _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state).Unwrap();
        public static Task<TNewResult> ContinueWith<TState, TNewResult>(this Task task, Func<Task, TState, Task<TNewResult>> continuationFunction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, cancellationToken).Unwrap();
        public static Task<TNewResult> ContinueWith<TState, TNewResult>(this Task task, Func<Task, TState, Task<TNewResult>> continuationFunction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, taskContinuationOptions).Unwrap();
        public static Task<TNewResult> ContinueWith<TState, TNewResult>(this Task task, Func<Task, TState, Task<TNewResult>> continuationFunction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, taskScheduler).Unwrap();
        public static Task<TNewResult> ContinueWith<TState, TNewResult>(this Task task, Func<Task, TState, Task<TNewResult>> continuationFunction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler).Unwrap();


        //continue method: Task<TNewResult> Continue(Task<T> task, TState state)
        public static Task<TNewResult> ContinueWith<T, TState, TNewResult>(this Task<T> task, Func<Task<T>, TState, Task<TNewResult>> continuationFunction, TState state)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state).Unwrap();
        public static Task<TNewResult> ContinueWith<T, TState, TNewResult>(this Task<T> task, Func<Task<T>, TState, Task<TNewResult>> continuationFunction, TState state, CancellationToken cancellationToken)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, cancellationToken).Unwrap();
        public static Task<TNewResult> ContinueWith<T, TState, TNewResult>(this Task<T> task, Func<Task<T>, TState, Task<TNewResult>> continuationFunction, TState state, TaskContinuationOptions taskContinuationOptions)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, taskContinuationOptions).Unwrap();
        public static Task<TNewResult> ContinueWith<T, TState, TNewResult>(this Task<T> task, Func<Task<T>, TState, Task<TNewResult>> continuationFunction, TState state, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, taskScheduler).Unwrap();
        public static Task<TNewResult> ContinueWith<T, TState, TNewResult>(this Task<T> task, Func<Task<T>, TState, Task<TNewResult>> continuationFunction, TState state, CancellationToken cancellationToken, TaskContinuationOptions taskContinuationOptions, TaskScheduler taskScheduler)
            => task.ContinueWith((Task<T> _task, object? _state) => continuationFunction.Invoke(_task, (TState)_state!), state, cancellationToken, taskContinuationOptions, taskScheduler).Unwrap();


#if DEBUG
        static async void Test()
        {
            //fix async () =>
            await Task.Factory.StartNewWithAsyncContext(async () => { });
            int a = await Task.Factory.StartNewWithAsyncContext(async () => { return 2; });

            Task<int> task = null;
            double d = await task!.ContinueWith(ContinueTest, "state");
        }

        static async Task<double> ContinueTest(Task<int> task, string state)
        {
            await Task.Delay(100);
            return 1.0;
        }
#endif
    }
}
