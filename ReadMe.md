# Memorize

A simple .NET Standard library to memoize a function with a single parameter

## Examples

- Memorize the result of a function taking a non-nullable object:

```cs
int MyComplexMathOperation(int i)
{
    Thread.Sleep(10000); // Sleep 10 seconds.
    return ++i;
}

var memoize = new Memorizer<int, int>(MyComplexMathOperation);
Console.WriteLine(memoize.Invoke(2));    // Takes some time
Console.WriteLine(memoize.Invoke(2));    // Instant
Console.WriteLine(memoize.Invoke(null)); // Compile error
```

- Memorize the result of a function taking a nullable object:

```cs
// inb4 deleting this breaks React
string MySlowAndWeirdLeftPad(string str)
{
    Thread.Sleep(10000); // Sleep 10 seconds.
    return str == null ? "Hello" : str.PadLeft(10);
}

var memoize = new Memorizer<string, string>(MySlowAndWeirdLeftPad);
Console.WriteLine(memoize.Invoke(null));     // Takes some time
Console.WriteLine(memoize.Invoke(null));     // Instant
Console.WriteLine(memoize.Invoke("pad me")); // Also works
```

Additional utilities are provided for your convenience:

- `bool IsResultMemorized(TParam param)`: Verify if a result has been memorized
- `void ClearMemorizedResults()`: Clear the cache
- `TResult GetMemorizedResult(TParam param)`: Get a memorized result
- `bool TryGetMemorizedResult(TParam param, out TResult result)`: Try getting a memorized result

Refer to the XML documentation for more info.