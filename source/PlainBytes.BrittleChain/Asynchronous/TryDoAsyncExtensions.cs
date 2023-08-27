using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the continue on success extensions.
    /// </summary>
    public static class TryDoAsyncExtensions
    {
        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Action<T> onHasValue)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }

        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Action<T> onHasValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value));
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }
        
        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Action<T, CancellationToken> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }

        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Action<T, CancellationToken> onHasValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await Task.Run(() => onHasValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }
        
        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Func<T, Task> onHasValue)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await onHasValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }
        
        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Func<T, Task> onHasValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await onHasValue(source.Value);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }

        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Func<T, CancellationToken, Task> onHasValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await onHasValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }
        
        public static async Task<Result<T>> TryDoAsync<T>(this Task<Result<T>> maybe, Func<T, CancellationToken, Task> onHasValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await onHasValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                }
            }

            return source;
        }
    }
}