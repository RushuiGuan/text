# Albatross.Text.CliFormat

A .NET library for formatting data using runtime expressions. Transform collections and objects into tables, CSV, JSON, and more using a simple expression syntax.

[Full Documentation](https://rushuiguan.github.io/text/)

## Installation

```bash
dotnet add package Albatross.Text.CliFormat
```

## Quick Start

```csharp
using Albatross.Text.CliFormat;

var people = new[] {
    new { Name = "John", Age = 30 },
    new { Name = "Jane", Age = 25 }
};

// Auto-detect format (prints as table)
Console.Out.CliPrint(people);

// Format expressions
Console.Out.CliPrint(people, "table(value, Name)");      // Table with Name only
Console.Out.CliPrint(people, "csv(value)");              // CSV output
Console.Out.CliPrint(people, "json(value)");             // JSON output
Console.Out.CliPrint(people, "first(value)");            // First item
```

## Operations Reference

| Operation | Example | Description |
|-----------|---------|-------------|
| `table` | `table(value, Col1, Col2)` | Tabular output with optional columns |
| `csv` / `ccsv` | `csv(value)` | CSV with/without headers |
| `json` | `json(value)` | JSON serialization |
| `list` | `list(value, Property)` | Key-value list format |
| `first` / `last` | `first(value)` | First/last element |
| `subset` | `subset(value, 0, 5)` | Extract range of items |
| `property` | `property(value, 'Name')` | Access object property |
| `cproperty` | `cproperty(value, Name)` | Extract property from each element |
| `jsonpointer` | `jsonpointer(value, /name)` | JSON pointer extraction |

## Chaining Operations

Operations can be chained - inner operations execute first:

```csharp
// Table of first 3 items with specific columns
Console.Out.CliPrint(data, "table(subset(value, 0, 3), Name, Age)");

// CSV export of names only
Console.Out.CliPrint(data, "csv(value, FirstName, LastName)");

// JSON of nested property
Console.Out.CliPrint(contact, "json(property(value, 'Address'))");
```

## Key Concepts

- **`value`** is the built-in variable referring to your input data
- Property names are **case-sensitive** in `property()` operations
- JSON pointer uses **camelCase** property names (`/firstName` not `/FirstName`)
- Customize table output via `TableOptionFactory.Instance.Register()`
