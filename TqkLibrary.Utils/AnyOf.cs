//Anthropic.AnyOf
using System;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
#if DEBUG
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public struct AnyOfResultAsync<T, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        public Func<T, TResult>? Value1 { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public Func<T, Task<TResult>>? Value2 { get; init; }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public struct AnyOf<T1, T2>
    {
        /// <summary>
        /// 
        /// </summary>
        public T1? Value1 { get; init; }
        /// <summary>
        /// 
        /// </summary>
        public T2? Value2 { get; init; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public AnyOf(T1? value1, T2? value2)
        {
            Value1 = value1;
            Value2 = value2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        public AnyOf(T1? value1) : this(value1, default) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value2"></param>
        public AnyOf(T2? value2) : this(default, value2) { }


        /// <summary>
        /// 
        /// </summary>
        public static implicit operator AnyOf<T1, T2>(T1 value) => new AnyOf<T1, T2>((T1?)value);
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator T1?(AnyOf<T1, T2> @this) => @this.Value1;
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator AnyOf<T1, T2>(T2 value) => new AnyOf<T1, T2>((T2?)value);
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator T2?(AnyOf<T1, T2> @this) => @this.Value2;
    }
#endif
}
