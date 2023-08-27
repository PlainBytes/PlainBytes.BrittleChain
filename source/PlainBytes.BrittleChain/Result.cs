using System;

namespace PlainBytes.BrittleChain
{
    /// <summary>
    /// Defines an operation result that either succeeds with a value, or fails. 
    /// </summary>
    /// <typeparam name="T">Type of the value if succeeded.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Indicates if the operation producing this result was successful.
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        /// Gets the operation result if it was successful.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Indicates if the operation producing this result failed.
        /// </summary>
        public bool Failed { get; }
        
        /// <summary>
        /// Gets the associated exception if the operation producing this result failed.
        /// </summary>
        public Exception Exception { get; }

        private Result(T value)
        {
            Value = value;
            Succeeded = true;
        }

        private Result(Exception exception)
        {
            Exception = exception;
            Failed = true;
        }

        /// <inheritdoc />
        public override string ToString() => Succeeded ? Value.ToString() : Exception.Message;

        /// <summary>
        /// Creates a <see cref="Result{T}"/> from the provided value.
        /// If value is <see langword="null"/> than it is still considered a failure.
        /// </summary>
        /// <param name="value">Value from which the result should be created.</param>
        public static Result<T> CreteFrom(T value)
        {
            if (value == null)
            {
                return new Result<T>(new ArgumentNullException(nameof(value)));
            }

            return new Result<T>(value);
        }

        /// <summary>
        /// Creates a <see cref="Result{T}"/> from the provided exception.
        /// </summary>
        /// <param name="value">Value from which the result should be created.</param>
        public static Result<T> CreteFrom(Exception value)
        {
            return new Result<T>(value);
        }
        
        /// <summary>
        /// Converts a value into a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Value from which the result should be created.</param>
        public static implicit operator Result<T>(T value) => CreteFrom(value);
        
        /// <summary>
        /// Attempts to converts a <see cref="Result{T}"/> into its value.
        /// If result failed than the contained exception will be thrown. 
        /// </summary>
        /// <param name="result"> Result from which the value should be taken. </param>
        /// <exception cref="Exception">Exception from which the result was created.</exception>
        public static implicit operator T(Result<T> result)
        {
            if (result.Failed)
            {
                throw result.Exception;
            }
            
            return result.Value;
        }

        /// <summary>
        /// Converts the provided exception into a <see cref="Result{T}"/>
        /// </summary>
        /// <param name="value">Exception from which the result should be created.</param>
        public static implicit operator Result<T>(Exception value) => CreteFrom(value);
    }
}