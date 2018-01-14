# Memorize

A simple .NET Standard library to memoize a function with a single parameter

## Examples

- Cache the result of a function returning a non-nullable object:

```cs
int MyComplexMathOperation(int i)
{
    Thread.Sleep(10000); // Sleep 10 seconds.
    return ++i;
}

var memoize = new Memorizer<int, int>(MyComplexMathOperation);
Console.WriteLine(memoize.Invoke(2)); // Takes some time
Console.WriteLine(memoize.Invoke(2)); // Instant
```

- Cache the result of a function returning a nullable object:

```cs
// inb4 deleting this breaks React
string MySlowAndWeirdLeftPad(string str)
{
    Thread.Sleep(10000); // Sleep 10 seconds.
    return str == "Hello" ? null : str.PadLeft(10);
}

var memoize = new NullableMemorizer<string, string>(MySlowAndWeirdLeftPad);
Console.WriteLine(memoize.Invoke("Hello")); // Takes some time
Console.WriteLine(memoize.Invoke("Hello")); // Instant
```

Additional utilities are provided for your convenience:

- `bool IsResultMemorized(T param)`: Verify if a result has been cached
- `void ClearMemorizedResults()`: Clear the cache
- `TResult GetMemorizedResult(T param)`: Get a cached result
- `bool TryGetMemorizedResult(T param, out TResult result)`: Try getting a cached result

Refer to the XML documentation for more info.