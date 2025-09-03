# Albatross.Text

A comprehensive collection of .NET libraries for string manipulation, text formatting, and tabular data presentation. This project provides powerful extension methods and utilities to work with strings, StringBuilder, TextWriter, and formatted output in .NET applications.

## Features

### Core Text Manipulation (Albatross.Text)
- **String Interpolation**: Template-based string replacement with `${variable}` syntax
- **String Extensions**: Comprehensive utility methods including:
  - Case conversion (ProperCase, CamelCase)
  - Pattern matching with glob patterns (`Like` method)
  - String trimming operations (TrimStart, TrimEnd)
  - Efficient text parsing with `TryGetText`
  - Decimal formatting with trailing zero removal
  - Markdown and Slack link generation
- **StringBuilder Extensions**: Enhanced StringBuilder functionality with EndsWith methods
- **TextWriter Extensions**: Fluent API for building formatted text output

### Table Formatting (Albatross.Text.Table)
- Convert collections of objects into formatted tabular strings
- Flexible table rendering with customizable formatting
- Support for various output formats and column alignment

### CLI Formatting (Albatross.Text.CliFormat)
- Runtime format expressions for dynamic output formatting
- JSON formatting capabilities with pointer support
- Extensible formatting system for command-line applications

## Prerequisites

- .NET SDK 8.0 or higher
- Compatible with:
  - .NET Standard 2.1
  - .NET 6.0
  - .NET 8.0
  - .NET 9.0

## Installation

### Using .NET CLI
```bash
# Install the core text manipulation library
dotnet add package Albatross.Text

# Install table formatting library
dotnet add package Albatross.Text.Table

# Install CLI formatting library
dotnet add package Albatross.Text.CliFormat
```

### Using Package Manager Console
```powershell
Install-Package Albatross.Text
Install-Package Albatross.Text.Table
Install-Package Albatross.Text.CliFormat
```

### Build from Source
```bash
# Clone the repository
git clone https://github.com/RushuiGuan/text.git
cd text

# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release
```

## Example Usage

### String Interpolation
```csharp
using Albatross.Text;
using System.Collections.Generic;

// Template-based string interpolation
var template = "Hello ${name}, welcome to ${location}!";
var values = new Dictionary<string, string> {
    { "name", "John" },
    { "location", "New York" }
};

var result = template.Interpolate(key => values[key]);
// Output: "Hello John, welcome to New York!"
```

### String Extension Methods
```csharp
using Albatross.Text;

// Case conversions
"HELLO WORLD".CamelCase();        // "hello WORLD"
"hello world".ProperCase();       // "Hello world"

// Pattern matching with glob patterns
"test.txt".Like("*.txt");         // true
"document.pdf".Like("*.txt");     // false

// String trimming
"prefix_content".TrimStart("prefix_");  // "content"
"content_suffix".TrimEnd("_suffix");    // "content"

// Decimal formatting (remove trailing zeros)
2000.56400000M.Decimal2CompactText();   // "2000.564"
```

### TextWriter Extensions (Fluent API)
```csharp
using Albatross.Text;
using System.IO;

var writer = new StringWriter();
writer.Append("Hello")
      .Space(2)
      .Append("World")
      .Tab()
      .AppendLine("!")
      .Append("Items: ");

var items = new[] { "apple", "banana", "cherry" };
writer.WriteItems(items, ", ", prefix: "[", postfix: "]");

// Output: "Hello  World\t!\nItems: [apple, banana, cherry]"
```

### Table Formatting
```csharp
using Albatross.Text.Table;

public class Contact {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

var contacts = new[] {
    new Contact { Id = 1, Name = "John Doe", Email = "john@example.com" },
    new Contact { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
};

// Convert to table format
var table = contacts.StringTable();
table.PrintConsole(); // Prints formatted table to console
```

### CLI Formatting
```csharp
using Albatross.Text.CliFormat;

// Print object with custom formatting
var contact = new Contact { Id = 1, Name = "John", Email = "john@example.com" };
contact.CliPrint("json"); // Prints as JSON format
```

## Project Structure

```
text/
├── Albatross.Text/                 # Core string manipulation library
│   ├── StringExtensions.cs         # String utility methods
│   ├── StringInterpolationExtensions.cs # Template interpolation
│   ├── StringBuilderExtensions.cs  # StringBuilder enhancements
│   └── TextWriterExtensions.cs     # TextWriter fluent API
├── Albatross.Text.Table/           # Table formatting library
├── Albatross.Text.CliFormat/       # CLI formatting library
├── Albatross.Text.Test/            # Unit tests
├── Sample/                         # Example applications
└── Prompts/                        # Documentation prompts
```

## Libraries

|Name|Description|NuGet Package|
|-|-|-|
|[Albatross.Text](./Albatross.Text/)|Core string manipulation library with extensions and utilities|[![NuGet Version](https://img.shields.io/nuget/v/Albatross.Text)](https://www.nuget.org/packages/Albatross.Text)|
|[Albatross.Text.CliFormat](./Albatross.Text.CliFormat/)|Runtime format expressions for dynamic text output|[![NuGet Version](https://img.shields.io/nuget/v/Albatross.Text.CliFormat)](https://www.nuget.org/packages/Albatross.Text.CliFormat)|
|[Albatross.Text.Table](./Albatross.Text.Table/)|Convert collections to formatted tabular strings|[![NuGet Version](https://img.shields.io/nuget/v/Albatross.Text.Table)](https://www.nuget.org/packages/Albatross.Text.Table)|

## Running Tests

### Run All Tests
```bash
# Run all tests in the solution
dotnet test

# Run tests with specific framework
dotnet test --framework net8.0

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Run Tests for Specific Project
```bash
# Run tests for the core library
dotnet test Albatross.Text.Test/Albatross.Text.Test.csproj
```

### Using PowerShell Build Scripts
```powershell
# Import the dev scripts module
Import-Module .\dev-scripts.psm1

# Run tests and build packages
Pack -directory . -skipTest:$false
```

## Contributing

We welcome contributions! Here's how you can help:

### Reporting Issues
1. Search existing issues to avoid duplicates
2. Create a new issue with:
   - Clear description of the problem
   - Steps to reproduce
   - Expected vs actual behavior
   - .NET version and operating system

### Submitting Pull Requests
1. **Fork** the repository
2. **Clone** your fork locally
3. **Create** a feature branch: `git checkout -b feature/your-feature-name`
4. **Make** your changes following the existing code style
5. **Add** tests for new functionality
6. **Run** tests: `dotnet test`
7. **Commit** your changes with clear messages
8. **Push** to your fork: `git push origin feature/your-feature-name`
9. **Create** a Pull Request with:
   - Clear description of changes
   - Link to related issues
   - Screenshots if applicable

### Development Guidelines
- Follow existing code style and conventions
- Add XML documentation for public APIs
- Include unit tests for new features
- Update README.md if adding new functionality
- Ensure all tests pass before submitting PR

### Code Style
- Use C# naming conventions
- Add nullable reference type annotations
- Include XML documentation for public members
- Follow the existing project structure

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

**Copyright (c) 2019 Rushui Guan**

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
