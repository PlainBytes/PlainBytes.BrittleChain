using System;

namespace PlainBytes.BrittleChain
{
    public class Maybe<T>
    {
        public T Value { get; }

        public Exception Exception { get; }

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

        public override string ToString()
        {
            return HasValue ? Value.ToString() : Exception.Message;
        }
    }
}