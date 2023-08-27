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
                return Result<T>.CreateFromException(exception);
            }
            
            return Result<T>.CreateFrom(value);
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