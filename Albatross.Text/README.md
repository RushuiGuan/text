# Albatross.Text

A .NET string manipulation library providing extension methods for strings, StringBuilder, and TextWriter.

[Full Documentation](https://rushuiguan.github.io/text/)

## Installation

```bash
dotnet add package Albatross.Text
```

## Quick Start

```csharp
using Albatross.Text;

// Case conversion
"HelloWorld".CamelCase();     // "helloWorld"
"hello".ProperCase();         // "Hello"

// Glob pattern matching
"hello.txt".Like("*.txt");    // true
"test".Like("t?st");          // true

// String interpolation with ${expression} syntax
"Hello ${name}!".Interpolate(expr => expr == "name" ? "World" : "");
// "Hello World!"

// TextWriter fluent API
writer.Append("Name").Space().Append("Age").AppendLine();

// StringBuilder extensions
var sb = new StringBuilder("Hello World");
sb.EndsWith("World");         // true
```

## Key Features

- **Case Conversion**: `CamelCase()`, `ProperCase()`
- **Pattern Matching**: `Like()` for glob patterns (`*`, `?`)
- **String Parsing**: `TryGetText()` for delimited text parsing
- **Interpolation**: `${expression}` syntax with custom evaluators
- **TextWriter Fluent API**: `Append()`, `Space()`, `Tab()`, `Comma()`, `WriteItems()`
- **StringBuilder Extensions**: `EndsWith()` for string and char
- **Formatting Helpers**: `MarkdownLink()`, `SlackLink()`, `Decimal2CompactText()`
