[`< Back`](../../../)

---

# Extensions

Namespace: Albatross.Text.Table

```csharp
public static class Extensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Extensions](./albatross/text/table/extensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **MarkdownTable&lt;T&gt;(IEnumerable&lt;T&gt;, TextWriter, TableOptions&lt;T&gt;)**

```csharp
public static void MarkdownTable<T>(IEnumerable<T> items, TextWriter writer, TableOptions<T> options)
```

#### Type Parameters

`T`<br>

#### Parameters

`items` IEnumerable&lt;T&gt;<br>

`writer` [TextWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.textwriter)<br>

`options` TableOptions&lt;T&gt;<br>

### **TruncateMarkdownLink(String, Int32)**

```csharp
public static string TruncateMarkdownLink(string markdownLink, int displayWidth)
```

#### Parameters

`markdownLink` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`displayWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TruncateSlackLink(String, Int32)**

```csharp
public static string TruncateSlackLink(string slackLink, int displayWidth)
```

#### Parameters

`slackLink` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`displayWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TruncateText(String, Int32)**

```csharp
public static string TruncateText(string text, int displayWidth)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`displayWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IsSimpleValue(Type)**

```csharp
public static bool IsSimpleValue(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

---

[`< Back`](../../../)
