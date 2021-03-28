using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Extensions
{
    /// <summary>
    /// Synchronous extensions for the <see cref="Maybe{T}"/> tp chain execute actions on it.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Wraps the provided value into a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="value">Value which will be contained</param>
        /// <typeparam name="T">Type of provided value.</typeparam>
        /// <returns>Instance of  <see cref="Maybe{T}"/> holding the provided value.</returns>
        public static Maybe<T> AsMaybe<T>(this T value) => FromValue(value);

        /// <summary>
        /// Wraps the provided value into a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="value">Value which will be contained</param>
        /// <typeparam name="T">Type of provided value</typeparam>
        /// <returns>Instance of  <see cref="Maybe{T}"/> holding the provided value.</returns>
        public static Maybe<T> FromValue<T>(T value)
        {
            if (value == null)
            {
                return FromException<T>(new ArgumentException($"{nameof(value)} can not be null"));
            }

            return new Maybe<T>(value);
        }

        /// <summary>
        /// Wraps the provided exception into a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="exception">Exception which will be contained</param>
        /// <typeparam name="T">Type of expected value</typeparam>
        /// <returns>Instance of  <see cref="Maybe{T}"/> holding the provided exception.</returns>
        public static Maybe<T> FromException<T>(Exception exception)
        {
            return new Maybe<T>(exception);
        }

        /// <summary>
        /// Executes the provided action with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static Maybe<T> Chain<T>(this Maybe<T> source, Action<T> onValue)
        {
            if (source.HasValue)
            {
                try
                {
                    onValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        /// Executes the provided action with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Its source if it was successful, new container with exception if it failed.</returns>
        public static Maybe<T> Chain<T>(this Maybe<T> source, Action<T> onValue, Action<Exception> onError)
        {
            if (source.HasValue)
            {
                try
                {
                    onValue(source.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    onError(ex);
                    return FromException<T>(ex);
                }
            }

            return source;
        }

        /// <summary>
        /// Executes the provided action with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <returns>Always returns its source, even if it fails.</returns>
        public static Maybe<T> Do<T>(this Maybe<T> source, Action<T> onValue)
        {
            if (source.HasValue)
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
        /// Executes the provided action with <see cref="Maybe{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided action.</param>
        /// <param name="onValue">Action which should be called.</param>
        /// <param name="onError">Action which is called if exception occurs.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        // <returns>Always returns its source, even if it fails.</returns>
        public static Maybe<T> Do<T>(this Maybe<T> source, Action<T> onValue, Action<Exception> onError)
        {
            if (source.HasValue)
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

        /// <summary>
        /// Executes the provided action with <see cref="Maybe{T}.Exception"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Exception, parameter for the provided action.</param>
        /// <param name="onError">Action which is called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        public static Maybe<T> OnFail<T>(this Maybe<T> source, Action<Exception> onError)
        {
            if (!source.HasValue)
            {
                onError(source.Exception);
            }

            return source;
        }

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
                try
                {
                    var operationResult = onValue(source.Value);
                    result = FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = FromException<TResult>(ex);
                }
            }
            else
            {
                result = FromException<TResult>(source.Exception);
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
        public static Maybe<TResult> Shape<T, TResult>(this Maybe<T> source, Func<T, TResult> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            if (source.HasValue)
            {
                try
                {
                    var operationResult = onValue(source.Value);
                    result = FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = FromException<TResult>(ex);
                }
            }
            else
            {
                result = FromException<TResult>(source.Exception);
            }

            return result;
        }
    }
}