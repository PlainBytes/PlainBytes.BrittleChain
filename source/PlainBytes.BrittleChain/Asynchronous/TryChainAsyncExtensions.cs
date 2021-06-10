using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PlainBytes.BrittleChain.Synchronous;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the continue on success extensions.
    /// </summary>
    public static class TryChainAsyncExtensions
    {
        /// <summary>
        ///  Attempts to execute the provided action asynchronously with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Action<T> onValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onValue(source.Value));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }
        
        /// <summary>
        ///  Attempts to execute the provided action asynchronously with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Action<T> onValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onValue(source.Value));
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        ///  Attempts to execute the provided action asynchronously with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Action<T, CancellationToken> onValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        ///  Attempts to execute the provided action asynchronously with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Action<T, CancellationToken> onValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await Task.Run(() => onValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }
        
        /// <summary>
        ///  Attempts to execute the provided Task with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, Task> onValue)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }
        
        /// <summary>
        ///  Attempts to execute the provided Task with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, Task> onValue, Action<Exception> onError)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onValue(source.Value);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        ///  Attempts to execute the provided Task with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task> onValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        ///  Attempts to execute the provided Task with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Maybe<T>> TryChainAsync<T>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task> onValue, Action<Exception> onError, CancellationToken token)
        {
            var source = await maybe;

            if (source.HasValue)
            {
                try
                {
                    await onValue(source.Value, token);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    return MaybeExtensions.FromException<T>(ex);
                }
            }

            return source;
        }
    }
}