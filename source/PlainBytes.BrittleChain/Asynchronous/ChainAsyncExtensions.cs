using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PlainBytes.BrittleChain.Extensions;

namespace PlainBytes.BrittleChain.Asynchronous
{
    public static class ChainAsyncExtensions
    {
        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Action<T> onHasValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }
        
        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Action<T> onHasValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value));
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }

        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Action<T, CancellationToken> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }

        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Action<T, CancellationToken> onHasValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }
        
        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, Task> onHasValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onHasValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }
        
        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, Task> onHasValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onHasValue(source.Value);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }

        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onHasValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }
        
        public static async Task<Maybe<T>> ChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task> onHasValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onHasValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return Maybe.FromException<T>(ex);
                }
            }

            return source;
        }
    }
}