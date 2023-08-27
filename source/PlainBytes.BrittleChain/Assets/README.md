##  Brittle chain

 Small opiniated library to simplify chaining actions and their error handling with the result pattern.


### The problem 
There are many cases such as application boundaries, IO operations, network calls, etc. where we the possibility of failure can be high.
If failure happens, this must be communicated back to the caller, which could be done by throwing, returning null or other "invalid" values.
Now if we do this, that must be checked somehow, most likely with a try-catch, null check, type check, all of this results in a lot of noise which has nothing to do with the actual problem that we are trying to resolve.

This pattern is nothing new, functional programing has solved it long time ago with [Monads](https://en.wikipedia.org/wiki/Monad_(functional_programming)).

### Maybe has value, maybe failed?
The `Result` type has a simple role, transport to ```Value``` or the ```Exception```. In either way defines a simple, unified way to make decisions.
```c#
if (result.Succeeded)
{
    // do somthing
}
else
{
    // we have failed
    LogError(result.Exception);
}
```
### Chained execution
We have improved our error checking, however this will still results in Check, Do, Check, Do pattern. This is what ```Brittle chain``` tries to simplify:
```c#
var result = data.ToResult()
                .Chain(ActionOne)
                .Chain(ActionTwo)
                .Chain(ActionThree);
```
This is a typical chain, if any of the actions return a result which failed, the rest of the actions will not be called. 

That is a bit cleaner but what else can we do?
```c#
var result = data.ToResult()
                .TryDo(ActionOne) // Try to execute, catch exception if fails but pass along your source.
                .TryChain(ActionTwo) // Try to execute, catch exception if fails.
                .Select(FunctionThree) // Do if has value and retrun a different {T}.
                .OnFail(LogError);
```
There are many possible scenarios especially when we start looking at Async operations as well.
```c#
var result = await data.AsMaybeAsync()
                .TryDoAsync(ActionOne) // ➡ await Task.Run(ActionOne), wrapped in a try-catch block
                .TryChainAsync(AsyncMethod) // ➡ await AsyncMethod(value), wrapped in a try-catch block
                .SelectAsync(FunctionThree); // ➡ return await FunctionThree(value)
```
There are many more overloads, please have a look at the samples app for more...

‼ DO NOT mix async with synchronous calls ‼

### Key words
 - Do: "JUST DO" ➡ does not change source, does not break chain.
 - Chain: "DO IF" ➡ Execute if has value, does change source if fails, breaks chain.
 - Select: "CONVERT IF" ➡ Execute if has value, always changes source, breaks chain.
 - Try: "MIGHT BLOW" ➡ wraps the call into a try catch.
 - Map: "ERROR" or "SUCCESS" ➡ maps the two outcomes to a callback 

