# Albatross.Text

A comprehensive .NET string manipulation library that provides powerful extension methods for working with strings, StringBuilder, and TextWriter classes.

## Features

### String Interpolation
Advanced string interpolation using `${expression}` syntax that allows dynamic value replacement:

```csharp
using Albatross.Text;

// Simple interpolation with dictionary
var template = "Hello ${name}, you have ${count} messages";
var values = new Dictionary<string, string> 
{
    ["name"] = "John",
    ["count"] = "5"
};

var result = template.Interpolate(key => values[key]);
// Output: "Hello John, you have 5 messages"
```

### String Extensions
Powerful string manipulation methods:

```csharp
using Albatross.Text;

// Convert to proper case
"hello world".ProperCase(); // "Hello world"

// Convert to camel case  
"HelloWorld".CamelCase(); // "helloWorld"

// Wildcard matching
"test.txt".Like("*.txt"); // true

// Trim custom strings from start/end
"prefixHelloSuffix".TrimStart("prefix"); // "HelloSuffix"
"prefixHelloSuffix".TrimEnd("Suffix"); // "prefixHello"
```

### StringBuilder Extensions
Enhanced StringBuilder functionality:

```csharp
using Albatross.Text;
using System.Text;

var sb = new StringBuilder("Hello World");
bool endsWithWorld = sb.EndsWith("World"); // true
bool endsWithChar = sb.EndsWith('d'); // true
```

### TextWriter Extensions
Fluent API for writing formatted text:

```csharp
using Albatross.Text;
using System.IO;

var writer = new StringWriter();
writer.Append("Hello")
      .Space()
      .Append("World")
      .Comma()
      .Tab()
      .AppendLine("Welcome!");
// Output: "Hello World,	Welcome!\n"

// Specialized character methods
writer.OpenParenthesis()
      .Append("content")
      .CloseParenthesis()
      .Dot();
// Output: "(content)."
```

## Prerequisites

- .NET SDK 6.0 or later
- Target frameworks supported:
  - .NET Standard 2.1
  - .NET 6.0
  - .NET 8.0
  - .NET 9.0

## Installation

### Package Manager
```bash
Install-Package Albatross.Text
```

### .NET CLI
```bash
dotnet add package Albatross.Text
```

### Build from Source
```bash
# Clone the repository
git clone https://github.com/RushuiGuan/text.git
cd text

# Restore dependencies
dotnet restore

# Build the project
dotnet build --configuration Release

# Run tests
dotnet test
```

## Usage Examples

### Basic String Interpolation
```csharp
using Albatross.Text;
using System.Collections.Generic;

var template = "User: ${username}, Email: ${email}";
var data = new Dictionary<string, string>
{
    ["username"] = "johndoe",
    ["email"] = "john@example.com"
};

var result = template.Interpolate(key => data.TryGetValue(key, out var value) ? value : "N/A");
Console.WriteLine(result); // "User: johndoe, Email: john@example.com"
```

### Advanced Text Processing
```csharp
using Albatross.Text;

// Chain multiple string operations
var processed = "  HELLO WORLD  "
    .TrimStart("  ")
    .TrimEnd("  ")
    .CamelCase()
    .PostfixIfNotNullOrEmpty('!');

Console.WriteLine(processed); // "helloWORLD!"
```

### Fluent TextWriter Usage
```csharp
using Albatross.Text;
using System.IO;

var writer = new StringWriter();
var result = writer
    .OpenSquareBracket()
    .Append("Item1")
    .Comma()
    .Space()
    .Append("Item2")
    .CloseSquareBracket()
    .ToString();

Console.WriteLine(result); // "[Item1, Item2]"
```

## Project Structure

```
Albatross.Text/
├── StringInterpolationExtensions.cs  # String interpolation functionality
├── StringExtensions.cs               # General string manipulation methods
├── StringBuilderExtensions.cs        # StringBuilder extension methods
├── TextWriterExtensions.cs          # Fluent TextWriter API
└── README.md                        # Project documentation
```

## Running Tests

The project includes comprehensive unit tests to ensure reliability:

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests for specific framework
dotnet test --framework net8.0
```

Test files are located in the `Albatross.Text.Test` project and cover:
- String interpolation scenarios
- String extension method functionality
- StringBuilder extensions
- TextWriter fluent API
- Edge cases and error handling

## Contributing

We welcome contributions! Please follow these guidelines:

1. **Fork** the repository on GitHub
2. **Create** a feature branch from `main`:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make** your changes with appropriate tests
4. **Ensure** all tests pass:
   ```bash
   dotnet test
   ```
5. **Commit** your changes with clear messages:
   ```bash
   git commit -m "Add: Brief description of your changes"
   ```
6. **Push** to your fork and **submit** a pull request

### Code Style
- Follow existing code conventions
- Add unit tests for new functionality
- Update documentation for public APIs
- Ensure code builds without warnings

### Issues
- Use GitHub Issues to report bugs or request features
- Provide detailed reproduction steps for bugs
- Include relevant code samples when possible

## License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

Copyright (c) 2019 Rushui Guan

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
