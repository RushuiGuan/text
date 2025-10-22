[`< Back`](../../)

---

# StringExtensions

Namespace: Albatross.Text

```csharp
public static class StringExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StringExtensions](./albatross/text/stringextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **ProperCase(String, CultureInfo)**

Return the text with the first character in upper case.

```csharp
public static string ProperCase(string text, CultureInfo culture)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CamelCase(String, CultureInfo)**

Return the camel case version of the text.
 The method will convert the leading upper case characters to lower case until it reaches a lower case character or the end of the string.
 
 Here are some examples:
 CUSIP =&gt; cusip
 BBYellow =&gt; bbYellow
 Test =&gt; test
 test =&gt; test

```csharp
public static string CamelCase(string text, CultureInfo culture)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`culture` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Like(String, String)**

match a string against a glob pattern. ? matches any single character and * matches any characters
 If dealing with file systems directly, please use `Microsoft.Extensions.FileSystemGlobbing` instead.
 The method will return false for null or empty text string. It will throw an ArgumentException if the 
 parameter `globPattern` is null.

```csharp
public static bool Like(string text, string globPattern)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string to be tested

`globPattern` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A glob pattern where ? matches any single character and * matches any characters

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **PostfixIfNotNullOrEmpty(String, Char)**

Postfix the specified character to the text if the text is not empty and does not end with the said character

```csharp
public static string PostfixIfNotNullOrEmpty(string text, char character)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`character` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ReplaceMultipleChars(String, Char, Char[])**

Replace multiple characters in a string with a single character

```csharp
public static string ReplaceMultipleChars(string text, char replacementCharacter, Char[] targetCharacters)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`replacementCharacter` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

`targetCharacters` [Char[]](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TrimStart(String, String)**

Remove the specified value from the start of the line if it exists.

```csharp
public static string TrimStart(string line, string value)
```

#### Parameters

`line` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TrimEnd(String, String)**

Remove the specified value from the end of the line if it exists.

```csharp
public static string TrimEnd(string line, string value)
```

#### Parameters

`line` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TryGetText(String, Char, Int32&, String&)**

Try to get the text from the line with the given delimiter. The method can be called repeatedly on the same line
 until the end of the line is reached. This method is faster than using String.Split. Good for parsing delimited text.
 But does not handle escaped delimiters.

```csharp
public static bool TryGetText(string line, char delimiter, Int32& offset, String& text)
```

#### Parameters

`line` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`delimiter` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

`offset` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`text` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Decimal2CompactText(Decimal)**

write a decimal number and remove its trailing zeros after the decimal point

```csharp
public static string Decimal2CompactText(decimal value)
```

#### Parameters

`value` [Decimal](https://docs.microsoft.com/en-us/dotnet/api/system.decimal)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Decimal2CompactText(String, CultureInfo)**

this method trims the trailing zeros of a decimal number in string format. Since
 the input is a string, it could contain thousands separator and the decimal point could be culture specific.
 Here is an example to format 2000.5640000 as 2,000.564.
 var d = 2000.56400000M;
 var text = d.ToString("#,0.#############################").Decimal2CompactText()

```csharp
public static string Decimal2CompactText(string numberText, CultureInfo cultureInfo)
```

#### Parameters

`numberText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`cultureInfo` [CultureInfo](https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MarkdownLink(String, String)**

```csharp
public static string MarkdownLink(string text, string url)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`url` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SlackLink(String, String)**

```csharp
public static string SlackLink(string text, string url)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`url` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

---

[`< Back`](../../)
