# Albatross Text Library

A collection of .NET libraries for string manipulation and text formatting.

---

## Albatross.Text

A string manipulation library providing extension methods for strings, StringBuilder, and TextWriter.

[![NuGet](https://img.shields.io/nuget/v/Albatross.Text)](https://www.nuget.org/packages/Albatross.Text)

**Key Features:**
- Case conversion (`CamelCase`, `ProperCase`)
- Glob pattern matching (`Like`)
- String interpolation with `${expression}` syntax
- TextWriter fluent API
- StringBuilder extensions

```csharp
"HelloWorld".CamelCase();              // "helloWorld"
"hello.txt".Like("*.txt");             // true
"Hello ${name}".Interpolate(e => "World");
```

---

## Albatross.Text.Table

A library for rendering collections as formatted text tables with automatic column width adjustment.

[![NuGet](https://img.shields.io/nuget/v/Albatross.Text.Table)](https://www.nuget.org/packages/Albatross.Text.Table)

**Key Features:**
- Auto-discover columns via reflection
- Fluent API for column configuration
- Width control with text truncation
- Factory pattern for reusable configurations
- Markdown table export

```csharp
var products = new[] { new { Name = "Laptop", Price = 999.99m } };
products.StringTable().Print(Console.Out);
```

---

## Albatross.Text.CliFormat

A library for formatting data using runtime expressions into tables, CSV, JSON, and more.

[![NuGet](https://img.shields.io/nuget/v/Albatross.Text.CliFormat)](https://www.nuget.org/packages/Albatross.Text.CliFormat)

**Key Features:**
- Expression-based formatting (`table`, `csv`, `json`, `list`)
- Chainable operations
- Property access with dot notation
- JSON pointer support
- Auto-detect output format

```csharp
Console.Out.CliPrint(data, "table(value, Name, Age)");
Console.Out.CliPrint(data, "csv(subset(value, 0, 5))");
Console.Out.CliPrint(data, "json(value)");
```

---

## Source Code

[GitHub Repository](https://github.com/RushuiGuan/text)
