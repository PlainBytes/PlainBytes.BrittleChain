using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    public static class ChainExtensions
    {
        /// <summary>
        /// Executes the provided action with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static Result<T> Chain<T>(this Result<T> source, Action<T> onValue)
        {
            if (source.Succeeded)
            {
                onValue(source.Value);
            }

            return source;
        }

        /// <summary>
        /// Executes the provided action with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static Result<T> TryChain<T>(this Result<T> source, Action<T> onValue)
        {
            if (source.Succeeded)
            {
                try
                {
                    onValue(source.Value);
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
        /// Executes the provided action with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static Result<T> TryChain<T>(this Result<T> source, Action<T> onValue, Action<Exception> onError)
        {
            if (source.Succeeded)
            {
                try
                {
                    onValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    onError(ex);
                    return ex;
                }
            }

            return source;
        }
    }
}