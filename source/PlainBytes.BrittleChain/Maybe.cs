using System;

namespace PlainBytes.BrittleChain
{
    /// <summary>
    /// Simple value object, allows transmission of value or exception.
    /// </summary>
    /// <typeparam name="T">Type of wrapped value.</typeparam>
    public class Maybe<T>
    {
        /// <summary>
        /// Stores the wrapped value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Stores the wrapped exception.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Indicates if the <see cref="Value"/> was set, true if it was otherwise false.
        /// </summary>
        public bool HasValue { get; }
	
        internal Maybe(T value)
        {
            Value = value;
            HasValue = true;
        }
	
        internal Maybe(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// <inheritdoc cref="object.ToString"/>
        /// </summary>
        /// <returns>If <see cref="Value"/> was set returns its <see cref="object.ToString()"/> otherwise the <see cref="Exception"/>s message.</returns>
        public override string ToString()
        {
            return HasValue ? Value.ToString() : Exception.Message;
        }
    }
}