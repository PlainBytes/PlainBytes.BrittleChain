using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    /// <summary>
    /// Synchronous extensions for the <see cref="Result{T}"/> tp chain execute actions on it.
    /// </summary>
    public static class MaybeExtensions
    {
      

        /// <summary>
        /// Executes the provided action with <see cref="Result{T}.Exception"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Exception, parameter for the provided action.</param>
        /// <param name="onError">Action which is called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        public static Result<T> OnFail<T>(this Result<T> source, Action<Exception> onError)
        {
            if (!source.Succeeded)
            {
                onError(source.Exception);
            }

            return source;
        }

        
    }
}