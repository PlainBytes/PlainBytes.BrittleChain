using System;
using System.Threading.Tasks;

namespace PlainBytes.BrittleChain
{
    /// <summary>
    /// Defines extension methods for <see cref="Result{T}"/>
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Wraps the provided value into a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Value which will be contained</param>
        /// <typeparam name="T">Type of provided value</typeparam>
        /// <returns>Instance of  <see cref="Result{T}"/> holding the provided value.</returns>
        public static Result<T> ToResult<T>(this T value)
        {
            if (value is Exception exception)
            {
                return Result<T>.FromException(exception);
            }
            
            return Result<T>.FromValue(value);
        }

        /// <summary>
        /// Maps the provided result to onSuccess and onFailure functions.
        /// </summary>
        /// <param name="result">Instance of <see cref="Result{T}"/> which should be evaluated.</param>
        /// <param name="onSuccess">Action which should be executed if the result is successful.</param>
        /// <param name="onError">Action which should be executed if the result has failed.</param>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <returns>Source instance of <see cref="Result{T}"/></returns>
        public static Result<T> Map<T>(this Result<T> result, Action<T> onSuccess, Action<Exception> onError = null)
        {
            if (result.Succeeded)
            {
                onSuccess(result);
            }
            else
            {
                onError?.Invoke(result.Exception);
            }
            
            return result;
        }
        
        /// <summary>
        /// Maps the provided result to a specific exception.
        /// </summary>
        /// <param name="value">Instance of <see cref="Result{T}"/> which should be evaluated.</param>
        /// <param name="onError">Action which should be executed if the result has failed.</param>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <typeparam name="TException">Type of the exception that should be acted upon.</typeparam>
        /// <returns>Source instance of <see cref="Result{T}"/></returns>
        public static Result<T> WhenFailedWith<T, TException>(this Result<T> value, Action<TException> onError)
        {
            if (value.Failed && value.Exception is TException exception)
            {
                onError(exception);
            }

            return value;
        }
        
        /// <summary>
        /// Wraps the provided value into a <see cref="Result{T}"/> than a <see cref="value"/>.
        /// </summary>
        /// <param name="value">Value which will be contained</param>
        /// <typeparam name="T">Type of provided value</typeparam>
        /// <returns>Instance of  <see cref="Task{TResult}"/> holding the provided value.</returns>
        public static Task<Result<T>> ToResultTask<T>(this T value) => Task.FromResult(value.ToResult<T>());
    }
}