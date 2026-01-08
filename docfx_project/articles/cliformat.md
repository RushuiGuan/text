# Albatross.Text.CliFormat
A .NET library that provides flexible text formatting for CLI applications using runtime format expressions. Transform collections and objects into various output formats like tables, CSV, JSON, and custom layouts through a expression-based system.

## Features
- **Runtime Expression Formatting** - Transform data using expression strings like `"table(value, FirstName)"` or `"csv(subset(value, 0, 5))"`
- **Auto-Detection** - Automatically formats collections as tables, objects as key-value pairs, and simple values as text
- **Chainable Operations** - Combine operations like `"table(subset(value, 0, 2), Name, Age)"` for powerful data transformations
- **Multiple Output Formats** - Built-in support for tables, CSV, JSON, key-value lists, and custom formats
- **Deep Property Access** - Navigate nested objects with dot notation (`Address.Street`) and array indexing (`Emails[0]`)
- **JSON Pointer Support** - Extract data using JSON pointer syntax (`/firstName`, `/addresses/0/street`)
- **Collection Processing** - Extract properties from all collection elements with `cproperty` and `cjsonpointer`
- **Flexible Table Customization** - Control column visibility, headers, and formatting via `TableOptionFactory`

## Prerequisites
- .NET SDK 8.0 or later

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

// Simple table output
Console.Out.CliPrint(people);
// Name Age City   
// -------------- 
// John 30  Boston 
// Jane 25  Seattle
// --------------

// Custom formatting with expressions  
Console.Out.CliPrint(people, "csv(value, Name, Age)");        // CSV export
Console.Out.CliPrint(people, "json(subset(value, 0, 1))");   // JSON of first item
Console.Out.CliPrint(people, "table(value, Name)");          // Table with Name column only
```
- Target framework: .NET 8.0
- Dependencies:
  - Albatross.Expression 4.0.0+
  - Albatross.Serialization.Json 9.0.0+
  - CsvHelper 33.1.0+


## Entry Point
The single entry point for this library is the extension method:

```csharp
TextWriter.CliPrint<T>(T value, string? format = null)
```

**Parameters:**
- `value` - The input data to be formatted and printed
- `format` - Optional expression string that controls output formatting (defaults to `"value"`)

**How it works:**
The shape of the output text is determined by both the type of input data and the format string you provide. When you call `CliPrint`, it:

1. **Evaluates the format expression** using the built-in `value` variable that holds your input data
2. **Auto-detects the appropriate output format** based on the result type:
   - **Simple values** (string, primitives, DateTime, Guid) → printed directly
   - **Generic collections** (`IEnumerable<T>`) → formatted as tables using `StringTable`
   - **JsonElement** → serialized with camelCase property names
   - **Other objects** → properties converted to key-value table format

**Basic Usage:**
```csharp
// These are equivalent - format defaults to "value"
Console.Out.CliPrint(42);
Console.Out.CliPrint(42, "value");

// More complex formatting
var people = new Person[] { new Person("John", "Doe") };
Console.Out.CliPrint(people, "table(value, FirstName)");  // Show only FirstName column
Console.Out.CliPrint(people, "csv(value)");              // Export as CSV
Console.Out.CliPrint(people, "json(value)");             // Export as JSON
```

## Basic Formatting Syntax and the Built-In `value` Variable
> **Key Concept:** The built-in variable `value` always refers to your input data. All format expressions operate on this `value` variable.

```csharp
new StringWriter().CliPrint(1, "value");
// or new StringWriter().CliPrint(1);  The default formatting is "value" and it can be omitted.
```
The expression variable `value` holds the value of the input data `1`. The API will just print `1` since it is a simple value.

## Simple Value
The api considers primitive types, string, date and time types as well as Guid as simple value types.  If the input is a collection of simple values, the api will print them line by line.
```csharp
var textArray = new string[] {
   "John", "Emily", "Jane"
};
var writer = new StringWriter();
writer.CliPrint(textArray);
```
Output
```text
John
Emily
Jane
```
## Printing Object
If the `value` is an object but not a collection, by default the api will print its properties using a key value format.  
```csharp
public record class Job {
   public Job(string title) {
      this.Title = title;
   }
   public string Title { get; }
}
public record class Person {
   public Name(string first, string last){
      this.FirstName = first;
      this.LastName = last;
   }
   public string FirstName { get; }
   public string LastName  { get; }
   public Job? Job { get; set; }
}
System.Console.Out.CliPrint(new Person("john", "smith"));
```
The code above will print
```
Key       Value
---------------
FirstName john
LastName  smith
---------------
```
## `json` Operation
The `json` operation can be used to convert the value to a JsonElement.  The api will always print `JsonElement` by serializing it to text first.  Serialization converts property names to camel case.
```csharp
System.Console.Out.CliPrint(new Person("john", "smith"), "json(value)");
```
The code above will print out
```json
{
   "firstName": "john",
   "lastName": "smith"
}
```
## `jsonpointer` Operation
The `jsonpointer` operation takes a required JSON pointer parameter (like `/firstName` or `/addresses/0/street`) and extracts the corresponding value from the input data.  The parser recognizes JSON pointer syntax directly so it is ok to write the pointer without creating a string.
> **IMPORTANT:** The api serializes data to json using camel case for property names and JSON pointer is case sensitive.
```csharp
var names = new Person[] {
   new Person("john", "smith"),
   new Person("jane", "doe"),
   new Person("ethan", "allen")
};
System.Console.Out.CliPrint(names,"'/0/firstName'");  // pointer is created in the form of string. OK
System.Console.Out.CliPrint(names,"/0/firstName");    // pointer is not a string, but the parser understands it
System.Console.Out.CliPrint(names,"/0/FirstName");    // pointer /0/FirstName will result a null value since the property name is converted to camel case by the serializer.
```
The code above will print out
```
"john"
"john"
null
```
## `property` Operation
Similar to the `jsonpointer` operation, the `property` operation allows accessing properties of the input value, including nested properties from child objects.

```csharp
// Basic property access
// Like C#, the property names are case sensitive
System.Console.Out.CliPrint(new Person("john", "smith"), "property(value, 'FirstName')");
// Output: john
System.Console.Out.CliPrint(person, "property(value, FirstName)");      // also works
System.Console.Out.CliPrint(person, "property(value, 'firstname')");    // firstname is not a property in class Person.  An exception will be thrown
```
With the exception of the builtin variable `value`, the parser treats other variable names the same as strings of the same text.  That's why the second `CliPrint` in the code snippet above works.

### Nested Property Access
For complex objects with nested properties, use dot notation:

```csharp
public class Contact {
    public string FirstName { get; set; }
    public Address HomeAddress { get; set; }
}

public class Address {
    public string Street { get; set; }
    public string City { get; set; }
}

var contact = new Contact { 
    FirstName = "John", 
    HomeAddress = new Address { Street = "123 Main St", City = "Boston" }
};
// Access nested properties with dot notation
System.Console.Out.CliPrint(contact, "property(value, 'HomeAddress.Street')");
// Output: 123 Main St

System.Console.Out.CliPrint(contact, "property(value, 'HomeAddress.City')");
// Output: Boston
```

### Array/Collection Property Access
Use bracket notation for arrays and collections:

```csharp
public class Contact {
    public string FirstName { get; set; }
    public string[] Emails { get; set; }
    public Address[] Addresses { get; set; }
}

public class Address {
    public string Street { get; set; }
    public string City { get; set; }
}

var contact = new Contact {
    FirstName = "John",
    Emails = new[] { "john@work.com", "john@home.com" },
    Addresses = new[] { 
        new Address { Street = "123 Work St", City = "Boston" },
        new Address { Street = "456 Home Ave", City = "Cambridge" }
    }
};

// Access first email
System.Console.Out.CliPrint(contact, "property(value, 'Emails[0]')");
// Output: john@work.com

// Access second email  
System.Console.Out.CliPrint(contact, "property(value, 'Emails[1]')");
// Output: john@home.com

// Access nested property in array
System.Console.Out.CliPrint(contact, "property(value, 'Addresses[0].Street')");
// Output: 123 Work St

System.Console.Out.CliPrint(contact, "property(value, 'Addresses[1].City')");
// Output: Cambridge
```

### Dictionary Property Access
For Dictionary properties, use bracket notation with the key.

```csharp
public class Employee {
    public string Name { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

var employee = new Employee {
    Name = "Alice",
    Metadata = new Dictionary<string, string> {
        ["Department"] = "Engineering",
        ["Level"] = "Senior",
        ["Location"] = "Remote"
    }
};

// Access dictionary values by key
System.Console.Out.CliPrint(employee, "property(value, 'Metadata[Department]')");
// Output: Engineering

System.Console.Out.CliPrint(employee, "property(value, 'Metadata[Level]')");
// Output: Senior
```
### Key Differences from `jsonpointer`
- **Syntax**: `property` uses dot notation and brackets, `jsonpointer` uses `/` separators
- **Property names**: both operations use class property names and both are case sensitive.  But `jsonpointer` converts the property name to camel case.
- **Array access**: `property` uses `[0]`, `jsonpointer` uses `/0`
- **String Output**: `json` or `jsonpointer` will output string with quotes around it: `"string_value"` while `property` does not.
- **Error Handling**: `jsonpointer` returns null value for an invalid pointer.  `property` will throw an exception.

```csharp
var contacts = new Contact[] { 
    new Contact { FirstName = "John", Emails = new[] { "john@test.com" } }
};

// These are equivalent but use different syntax:
System.Console.Out.CliPrint(contacts, "property(value, '[0].FirstName')");        // Case-sensitive, bracket notation, output: John
System.Console.Out.CliPrint(contacts, "jsonpointer(value, /0/firstName)");            // Case-sensitive, slash notation, output: "John"
```

## Print Object Collection
If the `value` is a generic collection, the api will print its public properties in a tabular format.  The api leverage the `Albatross.Text.Table` library to print out tabular data with aligned formatting.
> **IMPORTANT:** The API treats a value as a collection **only** if its type implements the generic `IEnumerable<T>` interface, including arrays and generic collections. Non-generic collections (such as `IEnumerable` without a type parameter) are not recognized as collections for tabular printing.
```csharp
var names = new Person[] {
   new Person("john", "smith"),
   new Person("jane", "doe"),
   new Person("ethan", "allen")
};
System.Console.Out.CliPrint(names);
```
The code above will print
```
FirstName LastName
------------------
john      smith
jane      doe
ethan     allen
------------------
```
### Customize the Tabular Format of Object Collection Printing
By default the api will print all public properties of a type as the columns of a table.  To customized it, register a [TableOptions](../Albatross.Text.Table/TableOptions.cs) instance using [TableOptionFactory](../Albatross.Text.Table/TableOptionFactory.cs).  The registration of `TableOptions` via the static instance `TableOptionFactory.Instance` is global and only needs to be done once.
```csharp
TableOptionFactory.Instance.Register(new TableOptions<Person>().Ignore(x => x.LastName));
```
If the line above is called first, the prior example will print the text below instead.
```
FirstName
---------
john     
jane     
ethan    
---------
```
## Chaining Operations for Complex Data Transformations

Operations can be chained together to create powerful data transformations. The inner operations execute first, with their results becoming input to outer operations:

```csharp
// Get first email from a contact
writer.CliPrint(contact, "property(value, 'Emails[0]')");

// Get table of first 2 contacts with specific columns  
writer.CliPrint(contacts, "table(subset(value, 0, 2), FirstName, LastName)");

// Extract nested property and format as JSON
writer.CliPrint(contact, "json(property(value, 'HomeAddress.Street'))");

// Complex chaining: Get names from first 3 items as CSV
writer.CliPrint(contacts, "csv(subset(value, 0, 3), FirstName, LastName)");

// JSON pointer extraction from array
writer.CliPrint(addresses, "jsonpointer(value, /0/street)");
```

### Pro Tip: Start Simple, Then Chain
```csharp
// Step 1: Basic table
"table(value, FirstName, LastName)"

// Step 2: Add filtering  
"table(subset(value, 0, 5), FirstName, LastName)"

// Step 3: Add property extraction for complex data
"table(subset(cproperty(value, Contact), 0, 5), FirstName, LastName)"
```

## Other Operations
1. `first` and `last`
   Both operations only take 1 parameter.  If its parameter is a collection, the operation will return the first or last element respectively.  If the parameter is not a collection, both operations will return the parameter itself.

   parameters:
   1. input value
1. `subset`
   The `subset` operation will take 2 or 3 parameters.  Its behavior is similar to the `string.substring` method.  But the subset operation will not return index out of bound exception.  It will just return an empty collection.
   > **NOTE:** The `subset` operation will always return a collection.  If the input value is not a collection, the `subset` operation will convert it to a collection with a single element first.

   parameters:
   1. input value
   1. starting index (0 based)
   1. count (optional.  If omitted, return all elements after the starting index)
1. `csv` and `ccsv`
   Both operations will convert the input value into csv strings.  `ccsv` stands for compact csv.  It doesn't show headers.  Both operations will accept any numbers of columns.  If specified, the operations will only print those columns.  The columns names should match the property name and they are case sensitive.  An exception will be thrown if the property is not found.

   parameters:
   1. input value
   1. Property1 name
   1. Property2 name
   1. ...
1. `cproperty`
   The `c` of `cproperty` stands for `collection`. `cproperty` operation extracts a specific property from each element of a collection and returns an array of those property values. If the input value is not a collection.  It will be converted to a collection of a single element.

   parameters:
   1. input value
   2. property name (case-sensitive)

   ```csharp
   var people = new Person[] {
      new Person("john", "smith"),
      new Person("jane", "doe"), 
      new Person("ethan", "allen")
   };

   // Extract FirstName from each person
   System.Console.Out.CliPrint(people, "cproperty(value, FirstName)");
   ```
   
   The code above will print:
   ```
   john
   jane
   ethan
   ```
1. `cjsonpointer`
   The `cjsonpointer` operation applies a JSON pointer to each element in a collection and returns an array of the extracted values. This operation first converts the input to a JSON array, then applies the specified JSON pointer to each element.

   Unlike `jsonpointer` which works on the entire input data, `cjsonpointer` processes each item in a collection individually and extracts the same JSON path from all of them.

   parameters:
   1. input collection
   2. JSON pointer path (like `/firstName` or `/addresses/0/street`)

   ```csharp
   var people = new Person[] {
      new Person("john", "smith"),
      new Person("jane", "doe"), 
      new Person("ethan", "allen")
   };

   // Extract firstName from each person using JSON pointer
   System.Console.Out.CliPrint(people, "cjsonpointer(value, /firstName)");
   ```
   
   The code above will print:
   ```
   "john"
   "jane"
   "ethan"
   ```
   > **IMPORTANT:** Like `jsonpointer`, this operation uses camelCase property names and is case-sensitive. 
1. `table`
   The `table` operation converts collection data into a formatted table with aligned columns and borders. This is the **default operation** automatically applied when you call `CliPrint` on a collection.

   By explicitly using the `table` operation, you can control which columns to display and their order.

   > **IMPORTANT:** The `table` operation returns a `StringTable` object, not a collection. This means it **cannot be chained** as input for other operations and should always be used as the final operation in a chain.

   parameters:
   1. input collection
   2. property name 1 (case sensitive, optional)
   3. property name 2 (case sensitive, optional)
   4. ... (additional property names as needed)

   ```csharp
   var people = new Person[] {
      new Person("john", "smith"),
      new Person("jane", "doe"), 
      new Person("ethan", "allen")
   };

   // Table with single column
   System.Console.Out.CliPrint(people, "table(value, FirstName)");
   ```
   
   The code above will print:
   ```
   FirstName
   ---------
   john     
   jane     
   ethan    
   ---------
   ```
1. `list`
   The `list` operation converts collection data into a key-value format where each object's properties are displayed as separate Key-Value pairs. This creates a detailed, property-by-property view of your data with clear separators between objects.

   > **IMPORTANT:** The `list` operation returns a collection of `Dictionary<string, object>` instances that `CliPrint` automatically formats as a two-column table with "Key" and "Value" headers.

   parameters:
   1. input collection
   2. property name (case sensitive, optional)

   **Full Object Display:**
   ```csharp
   var people = new Person[] {
      new Person("john", "smith") {
         Job = new Job("engineer")
      },
      new Person("jane", "doe") {
         Job = new Job("analyst")  
      },
      new Person("ethan", "allen")
   };

   // Show all properties for each person, note that ethan allen doesn't have a job
   System.Console.Out.CliPrint(people, "list(value)");
   ```
   
   The code above will print:
   ```
   Key       Value
   ------------------
   FirstName john
   LastName  smith
   Job.Title engineer
   ------------------
   FirstName jane
   LastName  doe
   Job.Title analyst
   ------------------
   FirstName ethan
   LastName  allen
   ------------------
   ```

   **Selected Single Property Display of Simple Values:**
   ```csharp
   // Show only FirstName property - returns simple values, not key-value pairs
   System.Console.Out.CliPrint(people, "list(value, FirstName)");
   ```
   
   Output:
   ```
   john
   jane
   ethan
   ```

   **Selected Single Property Display of Objects:**
   ```csharp
   // Show only Job property - returns key-value pairs for each object's properties
   System.Console.Out.CliPrint(people, "list(value, Job)");
   ```
   
   Output:
   ```
   Key   Value
   --------------
   Title engineer
   --------------
   Title analyst
   --------------
   --------------
   ```

## Quick Reference

### Core Operations Summary

| Operation | Syntax | Input | Output | Purpose |
|-----------|--------|-------|--------|---------|
| `table` | `table(collection, [cols...])` | Collection | StringTable | Tabular display with optional column filtering |
| `csv` | `csv(collection, [cols...])` | Collection | CSV string | CSV export with headers |
| `ccsv` | `ccsv(collection, [cols...])` | Collection | CSV string | Compact CSV without headers |
| `json` | `json(data)` | Any | JSON string | JSON serialization with camelCase properties |
| `list` | `list(collection, [col])` | Collection | Key-value pairs | Property listing format |
| `subset` | `subset(collection, start, [count])` | Collection | Collection | Extract range of items |
| `first` | `first(collection)` | Collection | Single item | Get first element |
| `last` | `last(collection)` | Collection | Single item | Get last element |
| `property` | `property(object, 'path')` | Object | Property value | Access nested properties with dot/bracket notation |
| `cproperty` | `cproperty(collection, prop)` | Collection | Array | Extract property from each element |
| `jsonpointer` | `jsonpointer(data, /path)` | Any | Extracted value | JSON pointer extraction (case-sensitive) |
| `cjsonpointer` | `cjsonpointer(collection, /pointer)` | Collection | JSON array | JSON array with field filtering |

### Common Patterns

```csharp
// Basic formatting
writer.CliPrint(data);                           // Auto-detect format
writer.CliPrint(data, "table(value, Name, Age)"); // Table with specific columns
writer.CliPrint(data, "json(value)");             // JSON output

// Data extraction
writer.CliPrint(contact, "property(value, 'Email')");        // Single property
writer.CliPrint(contacts, "cproperty(value, Name)"); // Property from each item
writer.CliPrint(data, "jsonpointer(value, /users/0/name)");   // JSON pointer

// Collection operations  
writer.CliPrint(items, "subset(value, 0, 5)");              // First 5 items
writer.CliPrint(items, "first(value)");                     // First item only
writer.CliPrint(items, "csv(subset(value, 0, 10), Name)");  // CSV of first 10 names

// Complex chaining
writer.CliPrint(data, "table(subset(value, 0, 3), FirstName, LastName)");
writer.CliPrint(contacts, "json(property(value, 'Address[0]'))");
```

### Error Handling Tips

- **Property names are case-sensitive** for `property()` operation
- **JSON pointers are case-sensitive** and use camelCase property names  
- **Collection operations** only work with `IEnumerable<T>` types
- **Index out of bounds** in `subset()` returns empty collection (no exception)
- **Missing properties** in table operations throw exceptions - use `property()` for safe access