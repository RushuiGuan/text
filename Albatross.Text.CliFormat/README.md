# Albatross.Text.CliFormat

A .NET library that provides flexible text formatting for CLI applications using runtime format expressions. Transform collections and objects into various output formats like tables, CSV, JSON, and custom layouts through a powerful expression-based system.

## Features

* **Expression-Based Formatting** - Use runtime format expressions to dynamically control output formatting
* **Multiple Output Formats**:
  - **Auto** - Automatically selects the best format (table for collections, property list for objects)
  - **Table** - Tabular output with optional column selection
  - **CSV/CompactCSV** - Comma-separated values with or without headers
  - **JSON/JsonArray** - JSON serialization with optional field extraction using JSON pointers
  - **List** - Simple line-by-line output with optional column filtering
  - **First/Last** - Extract and display first/last N items from collections
* **Flexible Data Access** - Built-in variable system with automatic property resolution
* **Console Width Adaptation** - Automatically adjusts output to fit console width
* **Custom Expression Parser** - Configurable parser supporting various literal types and operations

## Prerequisites

- .NET SDK 8.0 or later
- Target framework: .NET 8.0
- Dependencies:
  - Albatross.Expression 4.0.0+
  - Albatross.Serialization.Json 9.0.0+
  - CsvHelper 33.1.0+

## Installation

### Package Manager
```bash
Install-Package Albatross.Text.CliFormat
```

### .NET CLI
```bash
dotnet add package Albatross.Text.CliFormat
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

## Example Usage

### Quick Start - Auto Formatting

```csharp
using Albatross.Text.CliFormat;

// Sample data class
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
}

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 999.99m, Date = DateTime.Now },
    new Product { Id = 2, Name = "Mouse", Price = 25.50m, Date = DateTime.Now.AddDays(-1) }
};

// Auto format - automatically chooses table format for collections
products.CliPrint(null); // Uses "auto(value)" by default
```

### Specific Format Operations

```csharp
// Table format with specific columns
products.CliPrint("table(value, \"Name\", \"Price\")");

// CSV format with headers
products.CliPrint("csv(value, \"Name\", \"Price\")");

// Compact CSV without headers
products.CliPrint("ccsv(value, \"Id\", \"Name\")");

// JSON array format
products.CliPrint("jsonarray(value)");

// JSON array with specific fields using JSON pointers
products.CliPrint("jsonarray(value, \"/Name\", \"/Price\")");

// List format showing only names
products.CliPrint("list(value, \"Name\")");

// Get first 3 items
products.CliPrint("first(value, \"Name\", 3)");

// Get last 2 items
products.CliPrint("last(value, \"Name\", 2)");
```

### Working with Single Objects

```csharp
var singleProduct = products.First();

// Auto format for single object - shows property table
singleProduct.CliPrint(null);

// Custom property extraction
singleProduct.CliPrint("Name"); // Accesses the Name property directly
```

### Advanced Expression Usage

```csharp
// Using the built-in "value" variable
products.CliPrint("table(value)");

// Property access through variables
products.CliPrint("first(value, \"Name\", 1)");
```

### Custom Parser Configuration

```csharp
using Albatross.Text.CliFormat;

// Build a custom parser (normally handled internally)
var parser = Extensions.BuildCustomParser();
var expression = parser.Build("table(value, \"Name\", \"Price\")");

// Manual execution with custom context
var context = new CustomExecutionContext<List<Product>>(parser);
var result = expression.Eval(name => context.GetValue(name, products));
Console.WriteLine(result.ConvertToString());
```

## Format Expression Syntax

The library uses prefix notation for format expressions:

| Operation | Syntax | Description |
|-----------|--------|-------------|
| `auto(value)` | Auto-detect format | Tables for collections, property lists for objects |
| `table(value, [columns...])` | Tabular output | Optional column selection |
| `csv(value, [columns...])` | CSV with headers | Optional column selection |
| `compactcsv(value, [columns...])` | CSV without headers | Optional column selection |
| `list(value, [column])` | Simple list | Optional single column filter |
| `jsonarray(value, [pointers...])` | JSON array | Optional JSON pointer field extraction |
| `json(value, [/pointer])` | JSON object | Single object serialization |
| `first(value, [column], [count])` | First N items | Optional column and count |
| `last(value, [column], [count])` | Last N items | Optional column and count |

### Variable Access

- `value` - Built-in variable referring to the input data
- Property names can be accessed directly (e.g., `Name`, `Price`)
- JSON pointers for complex field extraction (e.g., `"/Property/SubProperty"`)

## Project Structure

```
Albatross.Text.CliFormat/
├── Extensions.cs                      # Main extension methods and parser builder
├── Operations/                        # Format operation implementations
│   ├── Auto.cs                       # Automatic format detection
│   ├── Table.cs                      # Tabular output formatting
│   ├── Csv.cs                        # CSV with headers
│   ├── CompactCsv.cs                 # CSV without headers
│   ├── Json.cs                       # JSON object serialization  
│   ├── JsonArray.cs                  # JSON array with field extraction
│   ├── List.cs                       # Simple list formatting
│   ├── First.cs                      # First N items extraction
│   ├── Last.cs                       # Last N items extraction
│   └── CustomExecutionContext.cs     # Expression evaluation context
├── CsvClassMap.cs                     # CSV column mapping helper
├── CustomVariableFactory.cs          # Variable resolution factory
├── JsonPointerLiteral.cs             # JSON pointer literal support
├── FormattedJsonSerialization.cs     # Pretty JSON serialization
├── CompactJsonSerialization.cs       # Compact JSON serialization
├── Albatross.Text.CliFormat.csproj   # Project file
└── README.md                          # This file
```

## How to Run Unit Tests

```bash
# Run all tests in the solution
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run only CliFormat related tests
dotnet test --filter "TestCliFormat"

# Run tests for specific target framework
dotnet test --framework net8.0
```

Tests are located in the `Albatross.Text.Test` project and cover:
- Expression parsing and evaluation
- Format operation functionality
- Edge cases and error handling
- Integration with various data types

## Contributing

We welcome contributions! Please follow these steps:

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
5. **Follow** existing code conventions and add XML documentation
6. **Commit** your changes with clear messages:
   ```bash
   git commit -m "Add: Brief description of your changes"
   ```
7. **Push** to your fork and **submit** a pull request

### Code Guidelines
- Follow existing naming conventions and code style
- Add unit tests for new formatting operations
- Include XML documentation for public APIs
- Ensure code builds without warnings
- Update README.md if adding new features

### Reporting Issues
- Use GitHub Issues to report bugs or request features
- Provide minimal reproduction code when possible
- Include expected vs. actual behavior
- Specify .NET version and relevant dependencies

## License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.

Copyright (c) 2019 Rushui Guan

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
