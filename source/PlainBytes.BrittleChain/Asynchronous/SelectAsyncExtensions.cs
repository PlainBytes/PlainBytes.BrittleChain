using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the convert on success extensions.
    /// </summary>
    public static class SelectAsyncExtensions
    {
        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Result{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Result{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> maybe, Func<T, TResult> onValue)
        {
            Result<TResult> result;

            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value));
                    result = operationResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = ex;
                }
            }
            else
            {
                result = source.Exception;
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function asynchronously with <see cref="Result{T}.Value"/>, if it has one and returns the result of the function..
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the functions result.</typeparam>
        /// <returns> A <see cref="Result{T}"/> with the functions result if it was successful, exception if it failed.</returns>
        public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> maybe, Func<T, CancellationToken, TResult> onValue, CancellationToken token)
        {
            Result<TResult> result;

            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value, token), token);
                    result = operationResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = ex;
                }
            }
            else
            {
                result = source.Exception;
            }

            return result;
        }

        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Result{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Result{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> maybe, Func<T, Task<TResult>> onValue)
        {
            Result<TResult> result;

            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    var operationResult = await onValue(source.Value);
                    result = operationResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = ex;
                }
            }
            else
            {
                result = source.Exception;
            }

            return result;
        }

        /// <summary>
        /// Executes the provided task asynchronously with <see cref="Result{T}.Value"/>, if it has one and returns the result of the task.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided task.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of the task result.</typeparam>
        /// <returns> A <see cref="Result{T}"/> with the tasks result if it was successful, exception if it failed.</returns>
        public static async Task<Result<TResult>> SelectAsync<T, TResult>(this Task<Result<T>> maybe, Func<T, CancellationToken, Task<TResult>> onValue, CancellationToken token)
        {
            Result<TResult> result;

            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    var operationResult = await onValue(source.Value, token);
                    result = operationResult;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = ex;
                }
            }
            else
            {
                result = source.Exception;
            }

            return result;
        }
    }
}