# Albatross.Text.Table

A .NET library for rendering collections as formatted text tables with automatic column width adjustment and customizable formatting.

[Full Documentation](https://rushuiguan.github.io/text/)

## Installation

```bash
dotnet add package Albatross.Text.Table
```

## Quick Start

```csharp
using Albatross.Text.Table;

public record Product(int Id, string Name, decimal Price);

var products = new List<Product> {
    new(1, "Laptop", 999.99m),
    new(2, "Mouse", 25.50m)
};

// Print table using auto-discovered columns
products.StringTable().Print(Console.Out);
```

Output:
```
Id Name   Price
-- ------ ------
1  Laptop 999.99
2  Mouse  25.5
------
```

## Custom Configuration

```csharp
// Configure specific columns and formatting
var options = new TableOptions<Product>()
    .SetColumn(x => x.Name)
    .SetColumn(x => x.Price)
    .Format(x => x.Price, "C2")
    .ColumnHeader(x => x.Name, "Product");

products.StringTable(options).Print(Console.Out);

// Register globally for reuse
TableOptionFactory.Instance.Register(options);
```

## Key Features

- **Auto-discovery**: Columns built automatically via reflection
- **Fluent API**: Configure columns, headers, formatting, and ordering
- **Width control**: Adjust table width with automatic text truncation
- **Factory pattern**: Register and reuse configurations globally
- **Markdown export**: `products.MarkdownTable(writer)`
