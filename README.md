# SpintaxSharp
C# Library that generates strings based on Spintax string

## Example
```csharp
var input = "Hello, {Tom|Bobby {Singer|Robot}}!";

var result = Spintax.GenerateAll(input);

// Results:
"Hello, Tom!",
"Hello, Bobby Singer!",
"Hello, Bobby Robot!"
```

## Features
 - [x] Nested Spintax<br/>
   ```Hello, {Tom|Bobby {Singer|Robot}}```
 - [x] Escape-sequences<br/>
   ```Hello, \{Tom\} \|```
