using System;

namespace TqkLibrary.Utils.Proxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProxy
#if NET5_0_OR_GREATER
        : IAsyncDisposable
#else
        : IDisposable
#endif
    {
        /// <summary>
        /// 
        /// </summary>
        public string Proxy { get; }
    }
}
