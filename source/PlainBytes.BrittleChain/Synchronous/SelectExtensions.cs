﻿using System;
using System.Diagnostics;

namespace PlainBytes.BrittleChain.Synchronous
{
    public static class SelectExtensions
    {
        /// <summary>
        /// Executes the provided function with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of returned Value</typeparam>
        /// <returns>A new container with the functions result, or with exception if it failed.</returns>
        public static Result<TResult> Select<T, TResult>(this Result<T> source, Func<T, TResult> onValue)
        {
            Result<TResult> result;

            if (source.Succeeded)
            {
                var operationResult = onValue(source.Value);
                result = operationResult;
            }
            else
            {
                result = source.Exception;
            }

            return result;
        }

        /// <summary>
        /// Executes the provided function with <see cref="Result{T}.Value"/>, only if it has one.
        /// </summary>
        /// <param name="source">Container of Value, parameter for the provided function.</param>
        /// <param name="onValue">Function which should be called.</param>
        /// <typeparam name="T">Type of Value</typeparam>
        /// <typeparam name="TResult">Type of returned Value</typeparam>
        /// <returns>A new container with the functions result, or with exception if it failed.</returns>
        public static Result<TResult> TrySelect<T, TResult>(this Result<T> source, Func<T, TResult> onValue)
        {
            Result<TResult> result;

            if (source.Succeeded)
            {
                try
                {
                    var operationResult = onValue(source.Value);
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