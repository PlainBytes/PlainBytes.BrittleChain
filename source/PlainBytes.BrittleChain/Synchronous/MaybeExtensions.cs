using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    /// <summary>
    /// Synchronous extensions for the <see cref="Maybe{T}"/> tp chain execute actions on it.
    /// </summary>
    public static class MaybeExtensions
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

        
    }
}