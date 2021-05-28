using System.Threading;
using PlainBytes.BrittleChain.Asynchronous;
using PlainBytes.BrittleChain.Synchronous;

// Do: "JUST DO" Execute if has value, DO NOT change source, DO NOT break chain
// Chain: "DO IF" Execute if has value, DOES change source if fails, DOES break chain
// Shape: "CONVERT IF" Execute if has value, ALWAYS changes source, DOES break chain
// Try: wraps the call into a try catch. 
// OnFail: executes only if source has exception.

var source = 123.AsMaybe();
var result = source
        
    .Do(value => { })
    .TryDo(value => { })
    .TryDo(value => { }, error => {})
    
    .Chain(value => { })
    .TryChain(value => { })
    .TryChain(value => { }, error => { })
    
    .Shape(value => value)
    .TryShape(value => value)
    .TryShape(value => value, error => { })
    
    .OnFail(error => { });

var asyncResult = result.AsMaybeAsync()
    
    .DoAsync(value => { })
    .DoAsync((value, token) => { }, CancellationToken.None)
    .TryDoAsync((value) => { }, error => { })
    .TryDoAsync((value, token) => { }, CancellationToken.None)
    .TryDoAsync((value, token) => { }, error => { }, CancellationToken.None)
    
    .ChainAsync(value => { })
    .ChainAsync((value, token) => { }, CancellationToken.None)
    .TryChainAsync((value) => { }, error => { })
    .TryChainAsync((value, token) => { }, CancellationToken.None)
    .TryChainAsync((value, token) => { }, error => { }, CancellationToken.None)
    
    .ShapeAsync(value => value)
    .ShapeAsync((value, token) => value, CancellationToken.None)
    .TryShapeAsync((value) => value, error => { })
    .TryShapeAsync((value, token) => value, CancellationToken.None)
    .TryShapeAsync((value, token) => value, error => { }, CancellationToken.None);