//Anthropic.AnyOf
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
#if DEBUG
    class AnyOfDelegateAsyncTest
    {
        async Task test()
        {
            AnyOfDelegateAsync<int> a = new AnyOfDelegateAsync<int>((x) => Console.WriteLine(x));
            a = new AnyOfDelegateAsync<int>((x) => Task.CompletedTask);
            await a.Delegate(2);
        }
    }
#endif

#if DEBUG
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct AnyOfDelegateAsync<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<T, Task> Delegate { get; init; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AnyOfDelegateAsync(Action<T> @delegate)
        {
            if (@delegate == null) throw new ArgumentNullException(nameof(@delegate));
            this.Delegate = (t) =>
            {
                @delegate(t);
                return Task.CompletedTask;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AnyOfDelegateAsync(Func<T, Task> @delegate)
        {
            this.Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        public static implicit operator AnyOfDelegateAsync<T>(Action<T> @delegate) => new AnyOfDelegateAsync<T>(@delegate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        public static implicit operator AnyOfDelegateAsync<T>(Func<T, Task> @delegate) => new AnyOfDelegateAsync<T>(@delegate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is AnyOfDelegateAsync<T> anyOfDelegateAsync &&
                ReferenceEquals(Delegate, anyOfDelegateAsync.Delegate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Delegate.GetHashCode();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public struct AnyOfDelegateAsync<T1, T2>
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<T1, T2, Task> Delegate { get; init; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AnyOfDelegateAsync(Action<T1, T2> @delegate)
        {
            if (@delegate == null) throw new ArgumentNullException(nameof(@delegate));
            this.Delegate = (t1, t2) =>
            {
                @delegate(t1, t2);
                return Task.CompletedTask;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AnyOfDelegateAsync(Func<T1, T2, Task> @delegate)
        {
            this.Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        public static implicit operator AnyOfDelegateAsync<T1, T2>(Action<T1, T2> @delegate) => new AnyOfDelegateAsync<T1, T2>(@delegate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegate"></param>
        public static implicit operator AnyOfDelegateAsync<T1, T2>(Func<T1, T2, Task> @delegate) => new AnyOfDelegateAsync<T1, T2>(@delegate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is AnyOfDelegateAsync<T1, T2> anyOfDelegateAsync &&
                ReferenceEquals(Delegate, anyOfDelegateAsync.Delegate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Delegate.GetHashCode();
        }
    }
#endif
}
