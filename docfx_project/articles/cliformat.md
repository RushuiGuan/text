# Albatross.Text.CliFormat

A .NET library for formatting data using runtime expressions. Transform collections and objects into tables, CSV, JSON, and more using a simple expression syntax.

## Installation

```bash
dotnet add package Albatross.Text.CliFormat
```

## Quick Start

```csharp
using Albatross.Text.CliFormat;

var people = new[] {
    new { Name = "John", Age = 30, City = "Boston" },
    new { Name = "Jane", Age = 25, City = "Seattle" }
};

// Auto-detect format (prints as table)
Console.Out.CliPrint(people);

// Format expressions
Console.Out.CliPrint(people, "table(value, Name, Age)");  // Specific columns
Console.Out.CliPrint(people, "csv(value)");               // CSV output
Console.Out.CliPrint(people, "json(value)");              // JSON output
```

---

## The `value` Variable

The built-in variable `value` always refers to your input data. All format expressions operate on this variable.

```csharp
Console.Out.CliPrint(42, "value");           // Prints: 42
Console.Out.CliPrint(data, "json(value)");   // Serialize data as JSON
Console.Out.CliPrint(list, "first(value)");  // Get first element
```

---

## Auto-Detection

When no format is specified, output format is determined by the data type:

| Data Type | Output Format |
|-----------|---------------|
| Simple values (string, int, DateTime, Guid) | Printed directly |
| Collections (`IEnumerable<T>`) | Formatted as table |
| JsonElement | Serialized JSON with camelCase |
| Other objects | Key-value table |

```csharp
// Simple value
Console.Out.CliPrint(42);
// Output: 42

// Collection of simple values
Console.Out.CliPrint(new[] { "John", "Jane", "Bob" });
// Output:
// John
// Jane
// Bob

// Single object
Console.Out.CliPrint(new { Name = "John", Age = 30 });
// Output:
// Key  Value
// ----------
// Name John
// Age  30
// ----------

// Collection of objects
Console.Out.CliPrint(people);
// Output:
// Name Age City
// ---------------
// John 30  Boston
// Jane 25  Seattle
// ---------------
```

---

## Output Format Operations

### table

Formats collections as aligned tables. Default operation for collections.

```csharp
// All columns
Console.Out.CliPrint(people, "table(value)");

// Specific columns (case-sensitive)
Console.Out.CliPrint(people, "table(value, Name, Age)");
```

Output:
```
Name Age
--------
John 30
Jane 25
--------
```

> **Note:** `table` returns a `StringTable` and should be the final operation in a chain.

### csv / ccsv

Exports as CSV. `ccsv` (compact CSV) omits headers.

```csharp
// CSV with headers
Console.Out.CliPrint(people, "csv(value)");
// Name,Age,City
// John,30,Boston
// Jane,25,Seattle

// CSV without headers
Console.Out.CliPrint(people, "ccsv(value)");
// John,30,Boston
// Jane,25,Seattle

// Specific columns
Console.Out.CliPrint(people, "csv(value, Name, Age)");
// Name,Age
// John,30
// Jane,25
```

### json

Serializes to JSON with camelCase property names.

```csharp
Console.Out.CliPrint(person, "json(value)");
```

Output:
```json
{
  "name": "John",
  "age": 30,
  "city": "Boston"
}
```

### list

Displays each object as key-value pairs with separators between objects.

```csharp
Console.Out.CliPrint(people, "list(value)");
```

Output:
```
Key  Value
----------
Name John
Age  30
City Boston
----------
Name Jane
Age  25
City Seattle
----------
```

With property filter:
```csharp
Console.Out.CliPrint(people, "list(value, Name)");
// John
// Jane
```

---

## Collection Operations

### first / last

Returns the first or last element from a collection.

```csharp
Console.Out.CliPrint(people, "first(value)");  // First person as key-value
Console.Out.CliPrint(people, "last(value)");   // Last person as key-value

// Chain with other operations
Console.Out.CliPrint(people, "json(first(value))");
```

### subset

Extracts a range of items. Similar to `string.Substring` but never throws out-of-bounds.

```csharp
// subset(collection, startIndex, [count])

Console.Out.CliPrint(people, "subset(value, 0, 2)");   // First 2 items
Console.Out.CliPrint(people, "subset(value, 1)");     // All items from index 1
Console.Out.CliPrint(people, "subset(value, 10, 5)"); // Empty if out of range
```

---

## Property Access Operations

### property

Accesses object properties using dot notation and bracket indexing. Case-sensitive.

```csharp
// Simple property
Console.Out.CliPrint(person, "property(value, 'Name')");
// Output: John

// Nested property
Console.Out.CliPrint(person, "property(value, 'Address.City')");
// Output: Boston

// Array indexing
Console.Out.CliPrint(person, "property(value, 'Emails[0]')");
// Output: john@example.com

// Combined
Console.Out.CliPrint(person, "property(value, 'Addresses[0].Street')");
// Output: 123 Main St

// Dictionary access
Console.Out.CliPrint(employee, "property(value, 'Metadata[Department]')");
// Output: Engineering
```

> **Note:** Property names are case-sensitive. Invalid properties throw exceptions.

### cproperty

Extracts a property from each element in a collection.

```csharp
Console.Out.CliPrint(people, "cproperty(value, Name)");
// John
// Jane
// Bob

// Chain with other operations
Console.Out.CliPrint(people, "table(cproperty(value, Address), City, State)");
```

---

## JSON Pointer Operations

### jsonpointer

Extracts data using RFC 6901 JSON Pointer syntax. Uses camelCase property names.

```csharp
// Single value extraction
Console.Out.CliPrint(person, "jsonpointer(value, /name)");
// Output: "John"

// Array indexing
Console.Out.CliPrint(people, "jsonpointer(value, /0/name)");
// Output: "John"

// Nested path
Console.Out.CliPrint(person, "jsonpointer(value, /address/city)");
// Output: "Boston"
```

> **Important:** JSON pointers use camelCase (`/firstName` not `/FirstName`).

### cjsonpointer

Applies a JSON pointer to each element in a collection.

```csharp
Console.Out.CliPrint(people, "cjsonpointer(value, /name)");
// "John"
// "Jane"
// "Bob"
```

---

## Formatting Operations

### cformat

Applies a .NET format string to each element in a collection.

```csharp
var prices = new[] { 10.5m, 20.75m, 15.00m };
Console.Out.CliPrint(prices, "cformat(value, 'C2')");
// $10.50
// $20.75
// $15.00
```

---

## Chaining Operations

Operations can be chained. Inner operations execute first.

```csharp
// Table of first 3 items with specific columns
Console.Out.CliPrint(data, "table(subset(value, 0, 3), Name, Age)");

// CSV of extracted properties
Console.Out.CliPrint(data, "csv(cproperty(value, Contact), FirstName, LastName)");

// JSON of first item
Console.Out.CliPrint(data, "json(first(value))");

// JSON pointer on subset
Console.Out.CliPrint(data, "cjsonpointer(subset(value, 0, 5), /name)");
```

### Building Complex Expressions

Start simple, then add operations:

```csharp
// Step 1: Basic table
"table(value)"

// Step 2: Filter columns
"table(value, Name, Age)"

// Step 3: Limit rows
"table(subset(value, 0, 10), Name, Age)"

// Step 4: Extract nested data
"table(cproperty(value, Contact), FirstName, Email)"
```

---

## Customizing Table Output

Register `TableOptions` to customize how types are displayed as tables.

```csharp
using Albatross.Text.Table;

var options = new TableOptions<Person>()
    .BuildColumnsByReflection()
    .Cast<Person>()
    .Ignore(x => x.Id)
    .ColumnHeader(x => x.FirstName, "First")
    .Format(x => x.Salary, "C2");

TableOptionFactory.Instance.Register(options);

// Now CliPrint uses registered options
Console.Out.CliPrint(people);
```

---

## property vs jsonpointer

| Aspect | property | jsonpointer |
|--------|----------|-------------|
| Syntax | Dot notation: `'Address.City'` | Slash notation: `/address/city` |
| Case | Original case: `FirstName` | camelCase: `firstName` |
| Array access | Brackets: `[0]` | Slash: `/0` |
| String output | No quotes: `John` | Quoted: `"John"` |
| Invalid path | Throws exception | Returns null |

```csharp
// Equivalent extractions, different syntax:
Console.Out.CliPrint(person, "property(value, 'FirstName')");
// Output: John

Console.Out.CliPrint(person, "jsonpointer(value, /firstName)");
// Output: "John"
```

---

## API Reference

### Output Format Operations

| Operation | Syntax | Description |
|-----------|--------|-------------|
| `table` | `table(value, [cols...])` | Formatted table with optional columns |
| `csv` | `csv(value, [cols...])` | CSV with headers |
| `ccsv` | `ccsv(value, [cols...])` | CSV without headers |
| `json` | `json(value)` | JSON serialization (camelCase) |
| `list` | `list(value, [property])` | Key-value list format |

### Collection Operations

| Operation | Syntax | Description |
|-----------|--------|-------------|
| `first` | `first(value)` | First element |
| `last` | `last(value)` | Last element |
| `subset` | `subset(value, start, [count])` | Extract range |

### Property Access Operations

| Operation | Syntax | Description |
|-----------|--------|-------------|
| `property` | `property(value, 'path')` | Access property (case-sensitive) |
| `cproperty` | `cproperty(value, prop)` | Extract property from each element |
| `jsonpointer` | `jsonpointer(value, /path)` | JSON pointer extraction |
| `cjsonpointer` | `cjsonpointer(value, /path)` | JSON pointer on each element |

### Formatting Operations

| Operation | Syntax | Description |
|-----------|--------|-------------|
| `cformat` | `cformat(value, 'format')` | Apply .NET format string to collection |

### Entry Point

```csharp
// Main API
TextWriter.CliPrint<T>(T value, string? format = null)

// With pre-parsed expression
TextWriter.CliPrintWithExpression<T>(T value, IExpression? expression)
```
