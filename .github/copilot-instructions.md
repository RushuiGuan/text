# Albatross Text Libraries - AI Coding Agent Instructions

## Architecture Overview

This repository contains three interconnected .NET libraries for text manipulation and formatting:

- **Albatross.Text** - Core string extensions and utilities (netstandard2.1+)  
- **Albatross.Text.Table** - Tabular data formatting with fluent configuration (net8.0+)
- **Albatross.Text.CliFormat** - Expression-based runtime formatting system (net8.0)

**Key Dependency Flow**: `Albatross.Text` ← `Albatross.Text.Table` ← `Albatross.Text.CliFormat` ← `Sample`

## Critical Patterns

### Table Configuration Factory Pattern
- Use `TableOptionFactory.Instance` singleton for global registration
- Types auto-register on first use via `FallBackRegistration<T>()` using reflection
- Simple value types get special treatment in `TrySimpleValueCollectionRegistration<T>()`
- Pattern: `new TableOptions<T>().BuildColumnsByReflection().Cast<T>()` for fluent API

### Expression-Based CLI Formatting  
- `CliPrint(writer, value, format_string)` is the main entry point
- Format strings use prefix notation: `"table(value, FirstName, LastName)"`
- Operations registered in `Extensions.BuildCustomParser()` via `PrefixExpressionFactory<T>`
- Built-in variable `"value"` refers to input data via `CustomExecutionContext<T>`

### Test Data Generation Pattern
- Tests use `Bogus` faker library consistently: `var faker = new Faker("en")`
- Sample data classes: `Contact.Random(faker)`, `Address.Random(faker)`
- Collection generation: `Enumerable.Range(1, count).Select(_ => T.Random(faker))`

## Essential Commands

```bash
# Build entire solution
dotnet build

# Run all tests (xUnit + FluentAssertions)
dotnet test

# Run interactive sample project
dotnet run --project Sample -- --help
dotnet run --project Sample -- collection contact --count 3 --format "table(value, FirstName)"

# Test specific patterns
dotnet test --filter "TestBuildingTableOptions"
dotnet test --filter "TestCliFormat"
```

## Key Files for Context

- `Albatross.Text.Table/TableOptionFactory.cs` - Singleton registration pattern
- `Albatross.Text.CliFormat/Extensions.cs` - Parser builder and main API
- `Albatross.Text.CliFormat/Operations/` - All format operations (Table, Csv, Json, etc.)
- `Albatross.Text.Test/TestBuildingTableOptions.cs` - Factory and fluent API patterns
- `Sample/` - Live demonstrations of CLI formatting features

## Configuration Conventions

- **Multi-targeting**: Core library supports netstandard2.1+, newer libs require net8.0+
- **Nullable**: All projects enable nullable reference types
- **Version**: Centralized in `Directory.Build.props` (currently 8.2.0)
- **Dependencies**: Heavy use of Albatross.* ecosystem (Expression, Reflection, CommandLine)

## Testing Approach

- **xUnit** with **FluentAssertions** for readable assertions
- **Parametrized tests** using `[Theory]` and `[InlineData]` extensively
- **Factory testing**: Always test both registration and retrieval patterns
- **String comparison**: Use `.NormalizeLineEnding()` for cross-platform compatibility
- **Sample project**: Contains live integration tests as CLI commands

## CLI Format Expression Syntax

Format operations use prefix notation with `value` as the built-in data variable. **Operations can be chained** for complex data transformations.

### Core Operations
- `"table(value, Column1, Column2)"` - Filtered table output
- `"csv(value)"` - CSV with headers, `"ccsv(value)"` - CSV without headers
- `"json(value)"` - Full JSON serialization
- `"list(value, Column)"` - Key-value list format
- `"first(value, N)"` / `"last(value, N)"` - Extract N items from collections
- `"property(value, 'path')"` - Access nested properties: `'address[0].street'`
- `"cproperty(value, propname)"` - Extract property from collection elements
- `"jsonpointer(value, /path/field)"` - JSON pointer extraction
- `"cjsonpointer(value, /pointer)"` - JSON array with field filtering

### Chaining Examples
```bash
# Get first email from a contact (tested)
dotnet run --project Sample -- single contact --format "first(property(value, 'Email'))"

# Get table of first 2 contacts with specific columns (tested)
dotnet run --project Sample -- collection contact --count 5 --format "table(first(value, 2), FirstName, LastName)"

# Access single property from object (tested)  
dotnet run --project Sample -- single contact --format "property(value, 'Email')"

# Extract property from collection elements
dotnet run --project Sample -- collection contact --format "cproperty(value, FirstName)"

# JSON pointer extraction from first item
dotnet run --project Sample -- use-case json-pointer  # Uses "jsonpointer(value, /0/street)"
```

### Complete Operations Reference

| Operation | Syntax | Input Type | Output | Example |
|-----------|--------|------------|---------|---------|
| **table** | `table(collection, [cols...])` | Collection | StringTable | `table(value, Name, Age)` |
| **csv** | `csv(collection, [cols...])` | Collection | CSV string | `csv(value, Name, Age)` |
| **ccsv** | `ccsv(collection, [cols...])` | Collection | CSV no headers | `ccsv(value, Name)` |
| **json** | `json(data)` | Any | JSON string | `json(value)` |
| **cjsonpointer** | `cjsonpointer(collection, pointer)` | Collection | JSON array | `cjsonpointer(value, /Name)` |
| **jsonpointer** | `jsonpointer(data, /path)` | Any | Extracted value | `jsonpointer(value, /0/street)` |
| **list** | `list(collection, [col])` | Collection | Key-value list | `list(value, Name)` |
| **first** | `first(collection, [count])` | Collection | First N items | `first(value, 3)` |
| **last** | `last(collection, [count])` | Collection | Last N items | `last(value, 2)` |
| **property** | `property(object, 'path')` | Object | Property value | `property(value, 'Email')` |
| **cproperty** | `cproperty(collection, prop)` | Collection | Property from elements | `cproperty(value, Name)` |

### Implementation Notes
- Operations registered in `Extensions.BuildCustomParser()` via `PrefixExpressionFactory<T>`
- Each operation extends `PrefixExpression` base class in `Operations/` folder
- Built-in `"value"` variable handled by `CustomExecutionContext<T>`
- Parser supports dot notation: `value.property.subproperty`
- JSON pointers use `/` syntax: `/property/0/field`
- **Chaining**: Inner operations execute first, outer operations process results
- **Auto-detection**: When format is `null`, uses table for collections, string representation for objects

### Adding New CLI Format Operations

1. **Create Operation Class** in `Operations/` folder:
   ```csharp
   public class MyOperation : PrefixExpression {
       public MyOperation() : base("myop", 1, 2) { } // name, min args, max args
       
       protected override object Run(List<object> operands) {
           var input = operands[0].ConvertToCollection(out var elementType);
           // Process input and return result
           return result;
       }
   }
   ```

2. **Register in Parser** (`Extensions.BuildCustomParser()`):
   ```csharp
   builder.AddFactory(new PrefixExpressionFactory<Operations.MyOperation>(false));
   ```

3. **Test with Sample Project**:
   ```bash
   dotnet run --project Sample -- collection contact --format "myop(value, param)"
   ```