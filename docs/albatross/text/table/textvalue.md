[`< Back`](../../../)

---

# TextValue

Namespace: Albatross.Text.Table

a data structure to hold a text value and its width. There are situations where the display width of the text is not the
 same as the actual width of the text. For example, if the text is a markdown link [Google](https://www.google.com), the 
 display width is only 6 , but the actual width is 30. The truncate method is used to truncate the text to fit a certain width.
 The default truncate method is [Extensions.TruncateText(String, Int32)](./albatross/text/table/extensions#truncatetextstring-int32). It simply truncates the text to the display width.
 See [Extensions.TruncateMarkdownLink(String, Int32)](./albatross/text/table/extensions#truncatemarkdownlinkstring-int32) and [Extensions.TruncateSlackLink(String, Int32)](./albatross/text/table/extensions#truncateslacklinkstring-int32) for examples of 
 trancating text such as a mark down link.

```csharp
public struct TextValue
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [TextValue](./albatross/text/table/textvalue)<br>
Implements [IEquatable&lt;TextValue&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Empty**

```csharp
public static TextValue Empty { get; }
```

#### Property Value

[TextValue](./albatross/text/table/textvalue)<br>

### **Truncate**

```csharp
public Func<string, int, string> Truncate { get; }
```

#### Property Value

[Func&lt;String, Int32, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<br>

### **Text**

```csharp
public string Text { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TextWidth**

```csharp
public int TextWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TextValue(String, Int32, Func&lt;String, Int32, String&gt;)**

```csharp
TextValue(string text, int textWidth, Func<string, int, string> truncate)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`textWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`truncate` [Func&lt;String, Int32, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<br>

### **TextValue(String)**

```csharp
TextValue(string text)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **GetText(Int32, Boolean)**

```csharp
string GetText(int displayWidth, bool alignRight)
```

#### Parameters

`displayWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`alignRight` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Equals(TextValue)**

```csharp
bool Equals(TextValue other)
```

#### Parameters

`other` [TextValue](./albatross/text/table/textvalue)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

---

[`< Back`](../../../)
