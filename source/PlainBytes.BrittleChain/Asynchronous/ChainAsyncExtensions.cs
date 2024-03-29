﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PlainBytes.BrittleChain.Synchronous;

namespace PlainBytes.BrittleChain.Asynchronous
{
    /// <summary>
    /// Contains the continue on success extensions.
    /// </summary>
    public static class ChainAsyncExtensions
    {
        /// <summary>
        /// Executes the provided action asynchronously with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Result<T>> ChainAsync<T>(this Task<Result<T>> maybe, Action<T> onValue)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                await Task.Run(() => onValue(source.Value));
            }

            return source;
        }

        /// <summary>
        /// Executes the provided action asynchronously with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Result<T>> ChainAsync<T>(this Task<Result<T>> maybe, Action<T, CancellationToken> onValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await Task.Run(() => onValue(source.Value, token), token);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return ex;
                }
            }

            return source;
        }

        /// <summary>
        /// Executes the provided Task with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Result<T>> ChainAsync<T>(this Task<Result<T>> maybe, Func<T, Task> onValue)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                try
                {
                    await onValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return ex;
                }
            }

            return source;
        }

        /// <summary>
        /// Executes the provided Task with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="maybe">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Task which should be called.</param>
        /// <param name="token">Cancellation token for the asynchronous operation.</param>
        /// <typeparam name="T">Type of Value.</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static async Task<Result<T>> ChainAsync<T>(this Task<Result<T>> maybe, Func<T, CancellationToken, Task> onValue, CancellationToken token)
        {
            var source = await maybe;

            if (source.Succeeded)
            {
                await onValue(source.Value, token);
            }

            return source;
        }
    }
}