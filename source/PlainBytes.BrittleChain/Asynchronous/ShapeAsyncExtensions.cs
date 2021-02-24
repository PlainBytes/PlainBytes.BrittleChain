using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using PlainBytes.BrittleChain.Extensions;

namespace PlainBytes.BrittleChain.Asynchronous
{
    public static class ShapeAsyncExtensions
    {
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> onValue)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value));
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, TResult> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value));
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, TResult> onValue, CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value, token), token);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, TResult> onValue, Action<Exception> onError,CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await Task.Run(() => onValue(source.Value, token), token);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Task<TResult>> onValue)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, Task<TResult>> onValue, Action<Exception> onError)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task<TResult>> onValue, CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value, token);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
        
        public static async Task<Maybe<TResult>> ShapeAsync<T, TResult>(this Task<Maybe<T>> maybe, Func<T, CancellationToken, Task<TResult>> onValue, Action<Exception> onError,CancellationToken token)
        {
            Maybe<TResult> result;

            var source = await maybe;
            
            if (source.HasValue)
            {
                try
                {
                    var operationResult = await onValue(source.Value, token);
                    result = Maybe.FromValue(operationResult);
                }
                catch (Exception ex)
                {
                    onError(ex);
                    Debug.WriteLine(ex.Message);
                    result = Maybe.FromException<TResult>(ex);
                }
            }
            else
            {
                result = Maybe.FromException<TResult>(source.Exception);
            }

            return result;
        }
    }
}