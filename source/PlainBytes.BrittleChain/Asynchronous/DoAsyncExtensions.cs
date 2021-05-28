using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the continue on success extensions.
    /// </summary>
    public static class DoAsyncExtensions
    {
        public static async Task<Maybe<T>> DoAsync<T>(this Task<Maybe<T>> maybe, Action<T> onHasValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                await Task.Run(() => onHasValue(source.Value));
            }

            return source;
        }

        public static async Task<Maybe<T>> DoAsync<T>(this Task<Maybe<T>> maybe, Action<T, CancellationToken> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                await Task.Run(() => onHasValue(source.Value, token), token);
            }

            return source;
        }

        public static async Task<Maybe<T>> DoAsync<T>(this Task<Maybe<T>> maybe, Func<T, Task> onHasValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                await onHasValue(source.Value);
            }

            return source;
        }

        public static async Task<Maybe<T>> DoAsync<T>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                await onHasValue(source.Value, token);
            }

            return source;
        }
    }
}