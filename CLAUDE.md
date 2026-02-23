# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build entire solution
dotnet build

# Run all tests (xUnit + FluentAssertions)
dotnet test

# Run specific test pattern
dotnet test --filter "TestBuildingTableOptions"
dotnet test --filter "TestCliFormat"

# Run interactive sample project
dotnet run --project Sample -- --help
dotnet run --project Sample -- collection contact --count 3 --format "table(value, FirstName)"

# Build documentation (requires docfx global tool)
dotnet tool update -g docfx
docfx docfx_project/docfx.json
```

## Architecture Overview

Three interconnected .NET libraries for text manipulation and formatting:

- **Albatross.Text** - Core string extensions and utilities (netstandard2.0+)
- **Albatross.Text.Table** - Tabular data formatting with fluent configuration (net8.0+)
- **Albatross.Text.CliFormat** - Expression-based runtime formatting system (net8.0+)

**Dependency Flow**: `Albatross.Text` ← `Albatross.Text.Table` ← `Albatross.Text.CliFormat` ← `Sample`

## Critical Patterns

### Table Configuration Factory Pattern
- Use `TableOptionFactory.Instance` singleton for global registration
- Types auto-register on first use via `FallBackRegistration<T>()` using reflection
- Pattern: `new TableOptions<T>().BuildColumnsByReflection().Cast<T>()` for fluent API
- Key file: `Albatross.Text.Table/TableOptionFactory.cs`

### Expression-Based CLI Formatting
- `CliPrint(writer, value, format_string)` is the main entry point
- Format strings use prefix notation: `"table(value, FirstName, LastName)"`
- Operations registered in `Extensions.BuildCustomParser()` via `PrefixExpressionFactory<T>`
- Built-in variable `"value"` refers to input data via `CustomExecutionContext<T>`
- Key file: `Albatross.Text.CliFormat/Extensions.cs`

### Adding New CLI Format Operations

1. Create operation class in `Albatross.Text.CliFormat/Operations/`:
   ```csharp
   public class MyOperation : PrefixExpression {
       public MyOperation() : base("myop", 1, 2) { } // name, min args, max args

       protected override object Run(List<object> operands) {
           var input = operands[0].ConvertToCollection(out var elementType);
           return result;
       }
   }
   ```

2. Register in `Extensions.BuildCustomParser()`:
   ```csharp
   builder.AddFactory(new PrefixExpressionFactory<Operations.MyOperation>(false));
   ```

## CLI Format Operations Reference

Format operations use prefix notation with `value` as the built-in data variable. Operations can be chained: `table(first(value, 3), Name, Age)`

| Operation | Syntax | Description |
|-----------|--------|-------------|
| **table** | `table(value, [cols...])` | Formatted table output |
| **csv** | `csv(value, [cols...])` | CSV with headers |
| **ccsv** | `ccsv(value, [cols...])` | CSV without headers |
| **json** | `json(value)` | JSON serialization |
| **list** | `list(value, [col])` | Key-value list format |
| **first** | `first(value, [count])` | First N items |
| **last** | `last(value, [count])` | Last N items |
| **subset** | `subset(value, start, [count])` | Extract range |
| **property** | `property(value, 'path')` | Access nested properties |
| **cproperty** | `cproperty(value, prop)` | Extract property from collection elements |
| **jsonpointer** | `jsonpointer(value, /path)` | JSON pointer extraction |
| **cjsonpointer** | `cjsonpointer(value, /path)` | JSON pointer on collection |
| **cformat** | `cformat(value, 'format')` | Apply .NET format strings |

## Test Data Generation

Tests use `Bogus` faker library consistently:
```csharp
var faker = new Faker("en");
var contacts = Enumerable.Range(1, count).Select(_ => Contact.Random(faker));
```

Sample data classes: `Contact.Random(faker)`, `Address.Random(faker)`, `Job.Random(faker)`

## Key Files

- `Albatross.Text.Table/TableOptionFactory.cs` - Singleton registration pattern
- `Albatross.Text.CliFormat/Extensions.cs` - Parser builder and main API
- `Albatross.Text.CliFormat/Operations/` - All format operations
- `Albatross.Text.Test/TestBuildingTableOptions.cs` - Factory and fluent API patterns
- `Sample/UserCases/` - Advanced CLI formatting examples
- `Directory.Build.props` - Centralized version and build configuration

## Configuration

- **Multi-targeting**: Core library supports netstandard2.0+, newer libs require net8.0+
- **Nullable**: All projects enable nullable reference types
- **Version**: Centralized in `Directory.Build.props`
- **Dependencies**: Uses Albatross.* ecosystem (Expression, Reflection, CommandLine)

## Testing

- xUnit with FluentAssertions for readable assertions
- Parametrized tests using `[Theory]` and `[InlineData]`
- Use `.NormalizeLineEnding()` for cross-platform string comparison
- Sample project contains live integration tests as CLI commands
