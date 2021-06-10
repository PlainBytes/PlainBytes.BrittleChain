using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PlainBytes.BrittleChain.Synchronous;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the convert on success extensions.
    /// </summary>
    public static class ShapeAsyncExtensions
    {
        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> onValue)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value));
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value));
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, TResult> onValue, CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value, token), token);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, TResult> onValue, Action<Exception> onError,CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value, token), token);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Task<TResult>> onValue)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Task<TResult>> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task<TResult>> onValue, CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value, token);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Maybe{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Maybe{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task<TResult>> onValue, Action<Exception> onError,CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value, token);
                    result = MaybeExtensions.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = MaybeExtensions.FromException<TResult>(ex);
                }
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }
    }
}