using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    public static class DoExtensions
    {
        /// <summary>
        /// Executes the provided action with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Always returns its source, even if it fails.</returns>
        public static Result<T> TryDo<T>(this Result<T> source, Action<T> onValue)
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
        // <returns>Always returns its source, even if it fails.</returns>
        public static Result<T> TryDo<T>(this Result<T> source, Action<T> onValue, Action<Exception> onError)
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
                }
            }

            return source;
        }
    }
}