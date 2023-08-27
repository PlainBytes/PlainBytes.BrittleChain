using System.Threading;
using PlainBytes.BrittleChain;
using PlainBytes.BrittleChain.Asynchronous;
using PlainBytes.BrittleChain.Synchronous;

// Do: "JUST DO" Execute if has value, DO NOT change source, DO NOT break chain
// Chain: "DO IF" Execute if has value, DOES change source if fails, DOES break chain
// Shape: "CONVERT IF" Execute if has value, ALWAYS changes source, DOES break chain
// Try: wraps the call into a try catch. 
// OnFail: executes only if source has exception.

var source = 123.ToResult();
var result = source
        
    .TryDo(value => { })
    .TryDo(value => { }, error => {})
    
    .Chain(value => { })
    .TryChain(value => { })
    
    .Select(value => value)
    .TrySelect(value => value);

var asyncResult = result.ToResultTask()

    .TryDoAsync((value) => { }, error => { })
    .TryDoAsync((value, token) => { }, CancellationToken.None)
    .TryDoAsync((value, token) => { }, error => { }, CancellationToken.None)

    .ChainAsync(value => { })
    .ChainAsync((value, token) => { }, CancellationToken.None)
    .TryChainAsync((value, token) => { }, CancellationToken.None)

    .SelectAsync(value => value)
    .SelectAsync((value, token) => value, CancellationToken.None)
    .TrySelectAsync((value, token) => value, CancellationToken.None);
