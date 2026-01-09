# Albatross.Text.Table

A .NET library that converts collections of objects into tabular string format with fluent interface. Print tabular data to console or any TextWriter with customizable width limitations and text truncation behavior for each column.

## Features

* **[StringTable](./StringTable.cs)** - Core class that stores tabular data in string format and provides methods to print data to a `TextWriter` with width limitations and customizable truncation behavior
* **[TableOptions\<T\>](./TableOptions.cs)** - Configuration class that defines how to convert instances of `IEnumerable<T>` into tabular text format
* **[TableOptionFactory](./TableOptionFactory.cs)** - Thread-safe factory class for managing and reusing `TableOptions<T>` instances with a global registry
* **Flexible Column Configuration** - Set custom headers, formatting, ordering, and data extraction for each column
* **Console Width Adaptation** - Automatically adjusts table width to fit console or custom width constraints
* **Text Truncation Control** - Configure truncation behavior individually for each column
* **Markdown Table Support** - Export collections as markdown tables
* **Dictionary and Array Support** - Built-in support for dictionaries and arrays

## Prerequisites

- .NET SDK 6.0 or later
- Target frameworks supported:
  - .NET 8.0
  - .NET 9.0

## Installation

### Package Manager
```bash
Install-Package Albatross.Text.Table
```

### .NET CLI
```bash
dotnet add package Albatross.Text.Table
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

### Quick Start - Print Collection as Table

```csharp
using Albatross.Text.Table;

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

// Simple table output - uses reflection to build columns automatically
products.StringTable().Print(Console.Out);
```

### Manual Column Configuration

```csharp
// Create table options with specific columns
var options = new TableOptions<Product>()
    .SetColumn(x => x.Id)
    .SetColumn(x => x.Name) 
    .SetColumn(x => x.Price);

var table = products.StringTable(options);
table.Print(Console.Out);
```

### Automatic Column Configuration with Reflection

```csharp
// Build columns automatically using reflection
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>();

// Apply formatting and customization
options.Format(x => x.Price, "C2")                    // Currency format
       .ColumnHeader(x => x.Name, "Product Name")      // Custom header
       .ColumnOrder(x => x.Date, -1);                  // Move Date to first position

var table = products.StringTable(options);
table.Print(Console.Out);
```

### Global Configuration with Factory

```csharp
// Register table options globally for reuse
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Format(x => x.Price, "C2");

TableOptionFactory.Instance.Register(options);

// Use registered options anywhere in your application
products.StringTable().Print(Console.Out); // Uses registered options automatically
```

### Dictionary and Array Support

```csharp
// Dictionary tables
var dict = new Dictionary<string, string> {
    { "Key1", "Value1" },
    { "Key2", "Value2" }
};
dict.StringTable().Print(Console.Out);

// String array tables  
var array = new string[] { "Item1", "Item2", "Item3" };
array.StringTable().Print(Console.Out);
```

### Markdown Table Export

```csharp
// Export as markdown table
var options = new TableOptions<Product>()
    .SetColumn(x => x.Id)
    .SetColumn(x => x.Name)
    .SetColumn(x => x.Price);

using var writer = new StringWriter();
products.MarkdownTable(writer, options);
Console.WriteLine(writer.ToString());
// Output: Id|Name|Price
//         -|-|-
//         1|Laptop|999.99
```

### Width Control and Truncation

```csharp
var table = products.StringTable();

// Set minimum width for specific columns
table.MinWidth(col => col.Name == "Name", 15)      // Minimum width for Name column
     .MinWidth(col => col.Name == "Price", 10)     // Minimum width for Price column
     .AdjustColumnWidth(80);                       // Total table width limit

table.Print(Console.Out);

// Right-align numeric columns
table.AlignRight(col => col.Name == "Price", true);
```

## How it Works

The library is built around a few key concepts:

### TableOptions\<T\> - Configuration Core
The generic `TableOptions<T>` class contains the configuration for transforming type T to string-based tabular data. It defines which columns to display, how to format values, and column ordering.

Key methods:
- **`SetColumn(x => x.Property)`** - Add a column using a property selector
- **`BuildColumnsByReflection()`** - Automatically discover all public properties
- **`Cast<T>()`** - Convert to strongly-typed options for fluent configuration
- **`Format(x => x.Property, "format")`** - Apply string formatting to columns
- **`ColumnHeader(x => x.Property, "Header")`** - Set custom column headers
- **`ColumnOrder(x => x.Property, order)`** - Control column display order

### TableOptionFactory - Global Registry
The `TableOptionFactory` class provides a thread-safe global registry for reusing table configurations across your application. It automatically creates default configurations when none are registered.

```csharp
// Register once, use everywhere
TableOptionFactory.Instance.Register(myOptions);
var table = myCollection.StringTable(); // Uses registered options
```

### StringTable - Output Generation
The `StringTable` class handles the actual table rendering with features like:
- Automatic width calculation and adjustment
- Text truncation with customizable behavior  
- Console width adaptation
- Support for any `TextWriter` output
- Column alignment control

### Extension Methods
The library provides convenient extension methods:
- **`StringTable()`** - Convert collections to tables
- **`MarkdownTable()`** - Export as markdown format
- **`Print()`** - Output to any TextWriter
- **`MinWidth()`** - Set column minimum widths
- **`AlignRight()`** - Control column text alignment

## Project Structure

```
Albatross.Text.Table/
├── StringTable.cs                    # Core table rendering class
├── StringTableExtensions.cs          # Extension methods for collections
├── TableOptions.cs                   # Configuration classes
├── TableOptionBuilder.cs             # Fluent builder for options
├── TableOptionFactory.cs             # Global registry for configurations
├── TextOptionBuilderExtensions.cs    # Builder helper extensions
├── TableColumnOption.cs              # Column-specific configuration
├── TextValue.cs                       # Text formatting utilities
├── Extensions.cs                      # General utility extensions
├── Assembly.cs                        # Assembly information
├── Albatross.Text.Table.csproj       # Project file
└── README.md                          # Project documentation
```

## API Reference

### Core Classes

- **`StringTable`** - Main table rendering class with width adjustment and formatting
- **`TableOptions<T>`** - Configuration for converting objects to table format  
- **`TableOptionFactory`** - Global registry for table configurations
- **`TextValue`** - Represents formatted text with display width information
- **`TableColumnOption`** - Individual column configuration

### Key Extension Methods

```csharp
// Collection to table conversion
IEnumerable<T>.StringTable(options?) -> StringTable
IDictionary.StringTable() -> StringTable

// Table output methods  
StringTable.Print(TextWriter)
StringTable.AdjustColumnWidth(int)
StringTable.MinWidth(predicate, width)
StringTable.AlignRight(predicate, align?)

// Markdown export
IEnumerable<T>.MarkdownTable(TextWriter, options?)
```

## Running Tests

The project includes comprehensive unit tests in the `Albatross.Text.Test` project:

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests for specific framework
dotnet test --framework net8.0
```

### Test Coverage Examples

The test project demonstrates various usage patterns:

```csharp
// Basic table creation (TestStringTable.cs)
var options = new TableOptions<TestClass>()
    .SetColumn(x => x.Id)
    .SetColumn(x => x.Name)
    .SetColumn(x => x.Value);

var table = objects.StringTable(options);

// Reflection-based column building (TestBuildingTableOptions.cs) 
var options = new TableOptions<TestClass>()
    .BuildColumnsByReflection()
    .Cast<TestClass>()
    .Format(x => x.Value, "0.00");

// Column width adjustment (TestStringTableColumnAdjustment.cs)
var table = new StringTable(headers);
table.AdjustColumnWidth(80); // Fit in 80 characters

// Dictionary and array tables (TestStringTable.cs)
var dict = new Dictionary<string, string> { {"Key1", "Value1"} };
dict.StringTable().Print(writer);

var array = new string[] { "Value1", "Value2" };  
array.StringTable().Print(writer);
```

Test coverage includes:
- String table creation and formatting
- Column width adjustment algorithms  
- Text truncation behavior
- TableOptions configuration and factory functionality
- Markdown table generation
- Dictionary and array table support
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