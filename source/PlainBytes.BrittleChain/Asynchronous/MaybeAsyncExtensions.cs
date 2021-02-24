using System.Threading.Tasks;
using PlainBytes.BrittleChain.Extensions;

namespace PlainBytes.BrittleChain.Asynchronous
{
    public static class MaybeAsyncExtensions
    {
        public static Task<Maybe<T>> AsMaybeAsync<T>(this Maybe<T> value) => Task.FromResult(value);
    }
}