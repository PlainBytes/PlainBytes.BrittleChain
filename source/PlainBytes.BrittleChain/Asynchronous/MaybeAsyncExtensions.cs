using System.Threading.Tasks;
using PlainBytes.BrittleChain.Extensions;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the extension methods to wrap and unwrap the <see cref="Maybe{T}"/>.
    /// </summary>
    public static class MaybeAsyncExtensions
    {
        /// <summary>
        /// Creates a completed task from the provided <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="value">Source object.</param>
        /// <typeparam name="T">Type of wrapped value.</typeparam>
        /// <returns>A completed task with the provided value.</returns>
        public static Task<Maybe<T>> AsMaybeAsync<T>(this Maybe<T> value) => Task.FromResult(value);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<Maybe<T>> AsMaybeAsync<T>(this T value) => Task.FromResult(value.AsMaybe());
    }
}