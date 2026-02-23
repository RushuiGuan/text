# Albatross.Text.Table

A .NET library for rendering collections as formatted text tables with automatic column width adjustment and customizable formatting.

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
    new(2, "Mouse", 25.50m),
    new(3, "Keyboard", 75.00m)
};

// Print table with auto-discovered columns
products.StringTable().Print(Console.Out);
```

Output:
```
Id Name     Price
-- -------- ------
1  Laptop   999.99
2  Mouse    25.5
3  Keyboard 75
------------------
```

---

## TableOptions Configuration

### Building Columns by Reflection

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>();

products.StringTable(options).Print(Console.Out);
```

### Manual Column Selection

```csharp
var options = new TableOptions<Product>()
    .SetColumn(x => x.Name)
    .SetColumn(x => x.Price);

products.StringTable(options).Print(Console.Out);
```

Output:
```
Name     Price
-------- ------
Laptop   999.99
Mouse    25.5
Keyboard 75
--------------
```

### Custom Headers

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .ColumnHeader(x => x.Id, "#")
    .ColumnHeader(x => x.Name, "Product Name")
    .ColumnHeader(x => x.Price, "Unit Price");
```

### Column Ordering

Lower order values appear first. Default order is based on property declaration.

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .ColumnOrder(x => x.Price, -1)   // Move Price to first
    .ColumnOrder(x => x.Name, 0)     // Name second
    .ColumnOrder(x => x.Id, 1);      // Id last
```

### Ignoring Columns

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Ignore(x => x.Id);  // Remove Id column
```

### Value Formatting

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Format(x => x.Price, "C2");  // Currency format: $999.99
```

### Custom Value Extraction

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .SetColumn(x => x.Name, product => product.Name.ToUpper());
```

### Custom Formatter with TextValue

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Format(x => x.Price, (product, value) =>
        new TextValue(((decimal)value!).ToString("C2")));
```

---

## TableOptionFactory

Thread-safe singleton for registering and reusing table configurations globally.

### Registering Options

```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Format(x => x.Price, "C2")
    .Ignore(x => x.Id);

// Register globally
TableOptionFactory.Instance.Register(options);

// Now all calls use registered options automatically
products.StringTable().Print(Console.Out);
```

### Retrieving Options

```csharp
// Get registered options (or auto-create default)
var options = TableOptionFactory.Instance.Get<Product>();
```

---

## StringTable

The core class for storing and rendering tabular data.

### Creating from Headers

```csharp
var table = new StringTable("Name", "Age", "City");
table.AddRow("John", "30", "Boston");
table.AddRow("Jane", "25", "Seattle");
table.Print(Console.Out);
```

### Width Adjustment

Automatically reduces column widths to fit within a maximum total width.

```csharp
var table = products.StringTable();
table.AdjustColumnWidth(60);  // Fit in 60 characters
table.Print(Console.Out);
```

### Column Minimum Width

Set minimum width to prevent columns from being truncated too much.

```csharp
var table = products.StringTable()
    .MinWidth(col => col.Name == "Name", 20)      // Name at least 20 chars
    .MinWidth(col => col.Name == "Price", 10);    // Price at least 10 chars

table.AdjustColumnWidth(50);
table.Print(Console.Out);
```

### Right Alignment

```csharp
var table = products.StringTable()
    .AlignRight(col => col.Name == "Price")
    .AlignRight(col => col.Name == "Id");

table.Print(Console.Out);
```

### Filtering Columns

```csharp
var table = products.StringTable()
    .FilterColumns("Name", "Price");  // Only show Name and Price

table.Print(Console.Out);
```

### Filtering Rows

```csharp
var table = products.StringTable()
    .FilterRows("Price", value => decimal.Parse(value) > 50);

table.Print(Console.Out);
```

### Print Options

```csharp
table.Print(
    Console.Out,
    printHeader: true,              // Show column headers
    printFirstLineSeparator: true,  // Line after header
    printLastLineSeparator: true    // Line after data
);
```

---

## Dictionary Tables

```csharp
var dict = new Dictionary<string, object> {
    { "Name", "John" },
    { "Age", 30 },
    { "City", "Boston" }
};

dict.StringTable().Print(Console.Out);
```

Output:
```
Key  Value
---- ------
Name John
Age  30
City Boston
-----------
```

---

## Markdown Export

```csharp
using var writer = new StringWriter();
products.MarkdownTable(writer);
Console.WriteLine(writer.ToString());
```

Output:
```
Id|Name|Price
-|-|-
1|Laptop|999.99
2|Mouse|25.5
3|Keyboard|75
```

With custom options:
```csharp
var options = new TableOptions<Product>()
    .SetColumn(x => x.Name)
    .SetColumn(x => x.Price);

products.MarkdownTable(writer, options);
```

---

## Aligning Multiple Tables

When displaying multiple tables, align their columns for consistent formatting.

### AlignFirst

Align all tables to match the first table's column widths.

```csharp
var tables = new[] {
    products1.StringTable(),
    products2.StringTable(),
    products3.StringTable()
};

tables.AlignFirst();

foreach (var table in tables) {
    table.Print(Console.Out);
}
```

### AlignAll

Use the maximum width across all tables for each column.

```csharp
tables.AlignAll();
```

---

## API Reference

### TableOptions&lt;T&gt;

| Method | Description |
|--------|-------------|
| `BuildColumnsByReflection()` | Auto-discover columns from properties |
| `Cast<T>()` | Convert to typed options for fluent API |
| `SetColumn(expression)` | Add column by property expression |
| `SetColumn(expression, getValue)` | Add column with custom value getter |
| `Ignore(expression)` | Remove column |
| `ColumnHeader(expression, header)` | Set custom header text |
| `ColumnOrder(expression, order)` | Set column display order |
| `Format(expression, format)` | Apply .NET format string |
| `Format(expression, formatter)` | Apply custom formatter |
| `PrintHeader(bool)` | Control header visibility |
| `PrintFirstLineSeparator(bool)` | Control header separator |
| `PrintLastLineSeparator(bool)` | Control footer separator |

### StringTable

| Method | Description |
|--------|-------------|
| `AddRow(values)` | Add data row |
| `Print(writer)` | Output to TextWriter |
| `AdjustColumnWidth(maxWidth)` | Fit table within width |
| `FilterColumns(columns)` | Show only specified columns |
| `FilterRows(column, predicate)` | Filter rows by value |

### StringTableExtensions

| Method | Description |
|--------|-------------|
| `StringTable()` | Create table from collection |
| `StringTable(options)` | Create with custom options |
| `MinWidth(predicate, width)` | Set column minimum width |
| `AlignRight(predicate)` | Right-align columns |
| `SetWidthLimit(width)` | Adjust to fit width |
| `PrintConsole(writer, maxWidth)` | Print with width limit |

### TableOptionFactory

| Method | Description |
|--------|-------------|
| `Instance` | Singleton instance |
| `Register(options)` | Register configuration |
| `Get<T>()` | Get or create configuration |
| `TryGet<T>(out options)` | Try to get existing configuration |
