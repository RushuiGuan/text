# Albatross.Text.Table

A .NET library that converts collections of objects into tabular string format with fluent interface. Print tabular data to console or any TextWriter with customizable width limitations and text truncation behavior for each column.

## Features

* **[StringTable](./StringTable.cs)** - Core class that stores tabular data in string format and provides methods to print data to a `TextWriter` with width limitations and customizable truncation behavior
* **[TableOptions\<T\>](./TableOptions.cs)** - Immutable, thread-safe configuration class that defines how to convert instances of `IEnumerable<T>` into tabular text format
* **[TableOptionFactory](./TableOptionFactory.cs)** - Thread-safe factory class for managing and reusing `TableOptions<T>` instances with a global registry
* **[TableOptionBuilder\<T\>](./TableOptionBuilder.cs)** - Fluent builder for creating `TableOptions<T>` instances with customizable column behavior
* **Flexible Column Configuration** - Set custom headers, formatting, ordering, and data extraction for each column
* **Console Width Adaptation** - Automatically adjusts table width to fit console or custom width constraints
* **Text Truncation Control** - Configure truncation behavior individually for each column

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

// Simple table output
products.StringTable().PrintConsole();
```

### Custom Table Configuration with Fluent Builder

```csharp
using Albatross.Text.Table;

// Create custom table options
var options = new TableOptionBuilder<Product>()
    .GetColumnBuildersByReflection()
    .Format("Price", "#,#0.00")                    // Format price column
    .ColumnHeader("Name", "Product Name")          // Custom header
    .ColumnOrder("Date", 1)                        // Set column order
    .Ignore("Id")                                  // Hide Id column
    .Build();

// Use custom options
products.StringTable(options).PrintConsole();
```

### Advanced Column Customization

```csharp
var options = new TableOptionBuilder<Product>()
    .GetColumnBuildersByReflection()
    .SetColumn("Category", p => p.Name.Contains("Computer") ? "Electronics" : "Accessories")
    .Format("Price", (product, value) => $"${value:N2}")
    .ColumnHeader("Price", "Cost (USD)")
    .ColumnOrder("Category", 0)  // Show category first
    .Build();

products.StringTable(options).PrintConsole();
```

### Global Configuration with Factory

```csharp
// Register table options globally
var builder = new TableOptionBuilder<Product>()
    .GetColumnBuildersByReflection()
    .Format("Price", "#,#0.00")
    .Ignore("Id");

TableOptionFactory.Instance.Register(builder);

// Use registered options anywhere in your application
products.StringTable().PrintConsole(); // Uses registered options automatically
```

### Width Control and Truncation

```csharp
var table = products.StringTable();

// Set custom width and column constraints
table.MinWidth(col => col.Name == "Name", 15)      // Minimum width for Name column
     .MinWidth(col => col.Name == "Price", 10)     // Minimum width for Price column
     .AdjustColumnWidth(80)                        // Total table width limit
     .Print(Console.Out);

// Print with custom width
var customWidth = 60;
table.AdjustColumnWidth(customWidth).PrintConsole();
```

## How it Works

The library is built around a few key concepts:

### TableOptions\<T\> - Configuration Core
The generic `TableOptions<T>` class contains the configuration for transforming type T to string-based tabular data. Each instance is immutable and thread-safe, making it suitable for concurrent applications.

### TableOptionBuilder\<T\> - Fluent Configuration
The `TableOptionBuilder<T>` provides a fluent interface for creating table configurations:
- **GetColumnBuildersByReflection()** - Automatically discovers properties via reflection
- **Format()** - Define custom formatting for specific columns
- **SetColumn()** - Custom data extraction functions
- **ColumnHeader()** - Override column headers
- **ColumnOrder()** - Control column display order
- **Ignore()** - Hide specific columns

### TableOptionFactory - Global Registry
The `TableOptionFactory` class provides a thread-safe global registry for reusing table configurations across your application. It automatically creates default configurations when none are registered.

### StringTable - Output Generation
The `StringTable` class handles the actual table rendering with features like:
- Automatic width calculation and adjustment
- Text truncation with customizable behavior
- Console width adaptation
- Support for any `TextWriter` output

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

Test coverage includes:
- String table creation and formatting
- Column width adjustment algorithms
- Text truncation behavior
- TableOptions and factory functionality
- Edge cases and error handling
- Performance scenarios

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