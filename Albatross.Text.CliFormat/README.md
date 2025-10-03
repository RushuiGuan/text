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
Console.Out.CliPrint(products, null); // No format specified, uses automatic detection
```

### Table Format Operations

```csharp
// Table format with all columns - auto-detected for collections
Console.Out.CliPrint(contacts, null);
// Output:
// FirstName LastName   AgeInDays Address          Email           Phone           Scores        
// -------------------------------------------------------------------------------------
// Elmore    Balistreri 33119     Sample.Address[] System.String[] System.String[] System.Int32[] 
// Sadye     Lynch      29744     Sample.Address[] System.String[] System.String[] System.Int32[] 
// -------------------------------------------------------------------------------------

// Table format with specific columns
Console.Out.CliPrint(contacts, "table(value, FirstName, LastName, AgeInDays)");
// Output:
// FirstName LastName AgeInDays
// ----------------------------
// Khalil    Moore    22973    
// Damien    Klocko   10579    
// ----------------------------
```

### CSV Format Operations

```csharp
// CSV format with headers and specific columns
Console.Out.CliPrint(contacts, "csv(value, FirstName, LastName, AgeInDays)");
// Output:
// FirstName,LastName,AgeInDays
// Keith,Rosenbaum,28018
// Buck,Sanford,25176
// Wyatt,Ebert,4080

// Compact CSV without headers (use ccsv, not compactcsv)
Console.Out.CliPrint(contacts, "ccsv(value, FirstName, LastName, AgeInDays)");
// Output:
// Ressie,Turcotte,34433
// Kenna,Sanford,13520
// Ryan,Toy,15371
```

### JSON Format Operations

```csharp
// JSON array format for collections
Console.Out.CliPrint(addresses, "json(value)");
// Output:
// [
//   {
//     "street": "3196 Elva Prairie",
//     "city": "Cruickshankview", 
//     "state": "New Hampshire",
//     "zip": "03337",
//     "country": "Namibia"
//   },
//   {
//     "street": "15910 Goyette Motorway",
//     "city": "Stromanberg",
//     "state": "Massachusetts", 
//     "zip": "69087-5398",
//     "country": "Gambia"
//   }
// ]

// JSON pointer extraction from collections
Console.Out.CliPrint(addresses, "jsonpointer(value, /0/street)");
// Output: "079 Annetta Shoals"

// JSON array format with field extraction (requires JSON pointer)
Console.Out.CliPrint(addresses, "jsonarray(value, /street)");
```

### List and Selection Operations

```csharp
// List with specific column (creates key-value format)
Console.Out.CliPrint(contacts, "list(value, FirstName)");  
// Output:
// Key Value  
// -----------
//     Amos   
// -----------
//     Bobbie
// ----------

// Get first N items from collection (returns table format)
Console.Out.CliPrint(contacts, "first(value, 2)");
// Output: 
// FirstName LastName AgeInDays Address          Email           Phone          
// -----------------------------------------------------------------------------
// Louie     Tromp    30695     Sample.Address[] System.String[] System.String[]
// Trace     King     23687     Sample.Address[] System.String[] System.String[]
// -----------------------------------------------------------------------------

// Get last N items from collection  
Console.Out.CliPrint(contacts, "last(value, 2)");
// Output: Similar table format with last 2 items
```

### Property Access and Element Operations

```csharp
// Access single property from collection elements
Console.Out.CliPrint(contacts, "elem_property(value, firstname)");

// Access nested property with complex path
Console.Out.CliPrint(people, "property(value, '[0].address[0].street')");

// Access object properties  
Console.Out.CliPrint(contacts, "elem_property(value, 'job')");
```

### Working with Dictionaries

```csharp
var dictionary = new Dictionary<int, string>
{
    [0] = "Alice Johnson", 
    [1] = "Bob Smith",
    [2] = "Carol Davis"
};

// Auto-format dictionary as table (no format string needed)
Console.Out.CliPrint(dictionary, null);
// Output:
// Key Value       
// ----------------
// 0   Alice Johnson
// 1   Bob Smith    
// 2   Carol Davis  
// ----------------
```

### Working with Single Objects

```csharp
var singleContact = contacts.First();

// Auto format for single object - shows string representation  
Console.Out.CliPrint(singleContact, null);
// Output: Contact { FirstName = Emmy, LastName = Harvey, AgeInDays = 32858, 
//         Address = Sample.Address[], Email = System.String[], Phone = System.String[], 
//         Scores = System.Int32[], Job = Job { Company = Runolfsdottir and Sons, 
//         Title = Internal Research Director, Years = 12 }, Dob = 10/18/1935 }

// Use JSON for better single object formatting
Console.Out.CliPrint(singleContact, "json(value)");
// Output: Full JSON representation with all properties expanded
```

### Using with Custom TextWriter

```csharp
using var writer = new StringWriter();

// Write to custom TextWriter instead of Console
writer.CliPrint(products, "table(value, Name, Price)");

string result = writer.ToString();
```

### Advanced - Custom Parser and Context

```csharp
// Build a custom parser (normally handled internally)
var parser = Extensions.BuildCustomParser();
var expression = parser.Build("table(value, Name, Price)");

// Manual execution with custom context
var context = new CustomExecutionContext<List<Product>>(parser);
var result = expression.Eval(name => context.GetValue(name, products));

// Output the result
Console.Out.CliPrint(result, null);
```

## Format Expression Syntax

The library uses prefix notation for format expressions with the `value` variable representing the input data:

| Operation | Syntax | Description | Example |
|-----------|--------|-------------|---------|
| **Auto** | `null` or empty | Auto-detect format based on data type | `Console.Out.CliPrint(data, null)` |
| **Table** | `table(value, [columns...])` | Tabular output with optional column selection | `table(value, Name, Price)` |
| **CSV** | `csv(value, [columns...])` | CSV with headers and optional column selection | `csv(value, Name, Price)` |
| **Compact CSV** | `ccsv(value, [columns...])` | CSV without headers | `ccsv(value, Id, Name)` |
| **List** | `list(value, [column])` | Simple list format with optional single column | `list(value, Name)` |
| **JSON** | `json(value)` | JSON object serialization | `json(value)` |
| **JSON Pointer** | `jsonpointer(value, pointer)` | Extract field using JSON pointer | `jsonpointer(value, /street)` |
| **JSON Array** | `jsonarray(value, pointer)` | JSON array with field extraction | `jsonarray(value, /Name)` |
| **First** | `first(value, [count])` | First N items from collection | `first(value, 3)` |
| **Last** | `last(value, [count])` | Last N items from collection | `last(value, 2)` |
| **Property** | `property(value, path)` | Access nested properties with complex paths | `property(value, '[0].address[0].street')` |
| **Element Property** | `elem_property(value, property)` | Access property from collection elements | `elem_property(value, firstname)` |

### Key Concepts

- **`value`** - Built-in variable referring to the input data
- **Column Names** - Property names can be referenced directly (e.g., `Name`, `Price`, `firstname`)
- **JSON Pointers** - Use JSON pointer notation for field extraction (e.g., `/street`, `/Name`)
- **Property Paths** - Complex nested property access using bracket notation
- **Auto-Detection** - When format is `null`, automatically selects table format for collections and property display for single objects

## API Reference

### Core Extension Method

```csharp
// Main extension method for formatting output
TextWriter.CliPrint<T>(T value, string? format)
```

### Available Operations

All operations are implemented as prefix expressions that can be used in format strings:

```csharp
// Table operations
"table(value)"                          // All columns
"table(value, Name, Price)"            // Specific columns

// CSV operations  
"csv(value)"                           // All columns with headers
"csv(value, Name, Price)"              // Specific columns with headers
"compactcsv(value, Id, Name)"          // No headers

// JSON operations
"json(value)"                          // Full JSON serialization
"jsonpointer(value, /street)"          // Extract using JSON pointer
"jsonarray(value, /Name)"              // Array with field extraction

// List and selection operations
"list(value)"                          // Simple list format
"list(value, Name)"                    // Single column list
"first(value, Name, 3)"                // First N items
"last(value, Name, 2)"                 // Last N items

// Property access operations
"elem_property(value, firstname)"       // Property from collection elements
"property(value, '[0].address[0].street')" // Complex nested property access
```

## Project Structure

```
Albatross.Text.CliFormat/
├── Extensions.cs                      # Main extension methods and parser builder
├── Operations/                        # Format operation implementations
│   ├── Table.cs                      # Tabular output formatting
│   ├── Csv.cs                        # CSV with headers
│   ├── CompactCsv.cs                 # CSV without headers
│   ├── Json.cs                       # JSON object serialization  
│   ├── JsonArray.cs                  # JSON array with field extraction
│   ├── JsonPointer.cs                # JSON pointer extraction
│   ├── List.cs                       # Simple list formatting
│   ├── First.cs                      # First N items extraction
│   ├── Last.cs                       # Last N items extraction
│   ├── Property.cs                   # Property access operations
│   ├── ElementProperty.cs            # Element property access
│   ├── CustomExecutionContext.cs     # Expression evaluation context
│   └── Extensions.cs                 # Collection conversion utilities
├── CsvClassMap.cs                     # CSV column mapping helper
├── CustomVariableFactory.cs          # Variable resolution factory
├── JsonPointerLiteral.cs             # JSON pointer literal support
├── JsonPointerLiteralFactory.cs      # JSON pointer literal factory
├── FormattedJsonSerialization.cs     # Pretty JSON serialization
├── CompactJsonSerialization.cs       # Compact JSON serialization
├── Albatross.Text.CliFormat.csproj   # Project file
└── README.md                          # This file
```

## Running Tests

The project includes test coverage in both the `Albatross.Text.Test` and `Sample` projects:

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

### Sample Project Usage Examples

The `Sample` project can be run to see live demonstrations of the CLI formatting:

```bash
# Run the sample project to see available commands
dotnet run --project Sample -- --help

# Table format with auto-detection (collections become tables)
dotnet run --project Sample -- collection contact --count 3
# Output: Full contact table with all properties

# Table with specific columns
dotnet run --project Sample -- collection contact --count 3 --format "table(value, FirstName, LastName, AgeInDays)"
# Output: Filtered table with selected columns

# CSV formats
dotnet run --project Sample -- collection contact --count 3 --format "csv(value, FirstName, LastName)"
dotnet run --project Sample -- collection contact --count 3 --format "ccsv(value, FirstName, LastName)"

# JSON format
dotnet run --project Sample -- collection address --count 2 --format "json(value)"

# Dictionary formatting  
dotnet run --project Sample -- dictionary --count 3
# Output: Key-value table format

# First/Last operations
dotnet run --project Sample -- collection contact --count 5 --format "first(value, 2)"
dotnet run --project Sample -- collection contact --count 5 --format "last(value, 2)"

# Use case demonstrations
dotnet run --project Sample -- use-case json-pointer
dotnet run --project Sample -- use-case table-array-property
dotnet run --project Sample -- use-case string-array-property

# Single object formatting
dotnet run --project Sample -- single contact
```

Code examples from the sample project:

```csharp
// From Sample/TestPrintCollection.cs - Collection formatting
Console.Out.CliPrint(contacts, options.Format);

// From Sample/UserCases/TestTableArrayProperty.cs - Table with specific columns  
writer.CliPrint(items, "table(value, firstname, lastname)");

// From Sample/UserCases/TestJsonPointerUserCase.cs - JSON pointer extraction
writer.CliPrint(addresses, "jsonpointer(value, /0/street)");

// From Sample/UserCases/TestElementPropertyWithStringValue.cs - Element property access
writer.CliPrint(contacts, "elem_property(value, firstname)");

// From Sample/UserCases/TestDictionary.cs - Dictionary auto-formatting
writer.CliPrint(dictionary, null);
```

### Test Coverage Areas

Tests cover:
- Expression parsing and evaluation
- All format operations (table, csv, json, list, etc.)
- Property access and JSON pointer extraction
- Dictionary and collection handling
- Edge cases and error scenarios
- Integration with Albatross.Text.Table for tabular output

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
