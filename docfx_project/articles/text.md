# Albatross.Text

A .NET string manipulation library providing extension methods for strings, StringBuilder, and TextWriter.

## Installation

```bash
dotnet add package Albatross.Text
```

## String Extensions

### Case Conversion

```csharp
// ProperCase - Capitalize first character
"hello".ProperCase();           // "Hello"
"HELLO".ProperCase();           // "HELLO"

// CamelCase - Convert leading uppercase to lowercase
"HelloWorld".CamelCase();       // "helloWorld"
"CUSIP".CamelCase();            // "cusip"
"BBYellow".CamelCase();         // "bbYellow"
"XMLParser".CamelCase();        // "xmlParser"
```

### Glob Pattern Matching

The `Like` method matches strings against glob patterns where `*` matches any characters and `?` matches a single character.

```csharp
"hello.txt".Like("*.txt");      // true
"hello.txt".Like("hello.*");    // true
"test".Like("t?st");            // true
"test".Like("t??t");            // true
"abc".Like("a*c");              // true
"ABC".Like("abc");              // true (case-insensitive)
```

### String Trimming

```csharp
// TrimStart - Remove prefix
"HelloWorld".TrimStart("Hello");    // "World"
"prefix_name".TrimStart("prefix_"); // "name"

// TrimEnd - Remove suffix
"file.txt".TrimEnd(".txt");         // "file"
"name_suffix".TrimEnd("_suffix");   // "name"
```

### Delimited Text Parsing

`TryGetText` provides efficient parsing of delimited text without allocating arrays like `String.Split`.

```csharp
string line = "John,Doe,30,Engineer";
int offset = 0;

while (line.TryGetText(',', ref offset, out string? field))
{
    Console.WriteLine(field);
}
// Output:
// John
// Doe
// 30
// Engineer
```

### Character Replacement

```csharp
// Replace multiple characters with a single character
"hello-world_test".ReplaceMultipleChars('_', '-', ' ');  // "hello_world_test"
```

### Postfix Helper

```csharp
// Add character if string doesn't end with it
"path".PostfixIfNotNullOrEmpty('/');    // "path/"
"path/".PostfixIfNotNullOrEmpty('/');   // "path/"
```

### Decimal Formatting

```csharp
// Remove trailing zeros
123.45000m.Decimal2CompactText();       // "123.45"
100.00m.Decimal2CompactText();          // "100"
```

### Link Formatting

```csharp
// Markdown link
StringExtensions.MarkdownLink("Click here", "https://example.com");
// "[Click here](https://example.com)"

// Slack link
StringExtensions.SlackLink("Click here", "https://example.com");
// "<https://example.com|Click here>"
```

---

## String Interpolation

The `Interpolate` method replaces `${expression}` placeholders with evaluated values.

### Basic Usage

```csharp
"Hello ${name}!".Interpolate(expr => expr == "name" ? "World" : "");
// "Hello World!"

"${greeting} ${name}!".Interpolate(expr => expr switch
{
    "greeting" => "Hi",
    "name" => "John",
    _ => ""
});
// "Hi John!"
```

### With Context Object

```csharp
var person = new { Name = "Alice", Age = 30 };

"Name: ${Name}, Age: ${Age}".Interpolate<dynamic>(
    (expr, ctx) => expr switch
    {
        "Name" => ctx.Name,
        "Age" => ctx.Age.ToString(),
        _ => ""
    },
    person,
    throwException: true
);
// "Name: Alice, Age: 30"
```

### Error Handling

```csharp
// Throw on evaluation failure
"Value: ${missing}".Interpolate(expr => throw new Exception("Not found"));
// Throws InvalidOperationException

// Silent failure - keeps placeholder
"Value: ${missing}".Interpolate<object?>(
    (expr, _) => throw new Exception("Not found"),
    null,
    throwException: false
);
// "Value: ${missing}"
```

---

## TextWriter Extensions

Fluent API for writing formatted text with method chaining.

### Basic Writing

```csharp
using var writer = new StringWriter();

writer.Append("Hello")
      .Space()
      .Append("World")
      .AppendLine();
// "Hello World\n"
```

### Conditional Writing

```csharp
bool showAge = true;
writer.Append("Name: John")
      .AppendIf(showAge, ", Age: 30")
      .AppendLine();
// "Name: John, Age: 30\n"
```

### Character Methods

```csharp
writer.Tab()                    // \t
      .Space(3)                 // "   "
      .Comma()                  // ,
      .Semicolon()              // ;
      .Dot()                    // .
      .OpenParenthesis()        // (
      .CloseParenthesis()       // )
      .OpenSquareBracket()      // [
      .CloseSquareBracket()     // ]
      .OpenAngleBracket()       // <
      .CloseAngleBracket();     // >
```

### Writing Collections

`WriteItems` joins collection elements with a delimiter (more efficient than `string.Join`).

```csharp
var items = new[] { "apple", "banana", "cherry" };

writer.WriteItems(items, ", ");
// "apple, banana, cherry"

// With prefix and postfix
writer.WriteItems(items, ", ", prefix: "[", postfix: "]");
// "[apple, banana, cherry]"

// With custom formatting
writer.WriteItems(items, " | ", (w, item) => w.Append(item.ToUpper()));
// "APPLE | BANANA | CHERRY"
```

### Boolean as Bit

```csharp
writer.AppendBooleanAsBit(true);   // "1"
writer.AppendBooleanAsBit(false);  // "0"
```

---

## StringBuilder Extensions

### EndsWith Methods

Check if StringBuilder content ends with a string or character without converting to string.

```csharp
var sb = new StringBuilder("Hello World");

sb.EndsWith("World");    // true
sb.EndsWith("Hello");    // false
sb.EndsWith('d');        // true
sb.EndsWith('o');        // false
```

---

## API Reference

### StringExtensions

| Method | Description |
|--------|-------------|
| `ProperCase()` | Capitalize first character |
| `CamelCase()` | Convert leading uppercase to lowercase |
| `Like(pattern)` | Glob pattern matching (`*`, `?`) |
| `TrimStart(value)` | Remove string prefix |
| `TrimEnd(value)` | Remove string suffix |
| `TryGetText(delimiter, ref offset, out text)` | Parse delimited text |
| `ReplaceMultipleChars(replacement, chars)` | Replace multiple characters |
| `PostfixIfNotNullOrEmpty(char)` | Add suffix if missing |
| `Decimal2CompactText()` | Remove trailing zeros |
| `MarkdownLink(text, url)` | Create markdown link |
| `SlackLink(text, url)` | Create Slack link |

### StringInterpolationExtensions

| Method | Description |
|--------|-------------|
| `Interpolate(func)` | Replace `${expr}` with evaluated values |
| `Interpolate<T>(func, value, throwException)` | Interpolate with context object |

### TextWriterExtensions

| Method | Description |
|--------|-------------|
| `Append(obj)` | Write object |
| `AppendIf(condition, obj)` | Conditional write |
| `AppendLine()` / `AppendLine(obj)` | Write with newline |
| `AppendChar(c, count)` | Write character(s) |
| `Tab()`, `Space()`, `Comma()`, etc. | Write specific characters |
| `WriteItems(items, delimiter)` | Join collection with delimiter |
| `AppendBooleanAsBit(value)` | Write bool as 1/0 |

### StringBuilderExtensions

| Method | Description |
|--------|-------------|
| `EndsWith(string)` | Check if ends with string |
| `EndsWith(char)` | Check if ends with character |
