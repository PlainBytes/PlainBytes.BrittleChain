using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    public static class ShapeExtensions
    {
        /// <summary>
        /// Executes the provided function with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of returned Value</typeparam>
        /// <returns>A new container with the functions result, or with exception if it failed.</returns>
        public static Maybe<TResult> Shape<T, TResult>(this Maybe<T> source, Func<T, TResult> onValue)
        {
            Maybe<TResult> result;

            if (source.HasValue)
            {
                var operationResult = onValue(source.Value);
                result = MaybeExtensions.FromValue(operationResult);
            }
            else
            {
                result = MaybeExtensions.FromException<TResult>(source.Exception);
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of returned Value</typeparam>
        /// <returns>A new container with the functions result, or with exception if it failed.</returns>
        public static Maybe<TResult> TryShape<T, TResult>(this Maybe<T> source, Func<T, TResult> onValue)
        {
            Maybe<TResult> result;

            if (source.HasValue)
            {
                try
                {
                    var operationResult = onValue(source.Value);
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
        /// Executes the provided function with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of returned Value</typeparam>
        /// <returns>A new container with the functions result, or with exception if it failed.</returns>
        public static Maybe<TResult> TryShape<T, TResult>(this Maybe<T> source, Func<T, TResult> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            if (source.HasValue)
            {
                try
                {
                    var operationResult = onValue(source.Value);
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