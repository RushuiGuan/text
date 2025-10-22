[`< Back`](../../../)

---

# TableOptionsExtension

Namespace: Albatross.Text.Table

```csharp
public static class TableOptionsExtension
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableOptionsExtension](./albatross/text/table/tableoptionsextension)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **ColumnOrder&lt;T&gt;(TableOptions&lt;T&gt;, String, Int32)**

```csharp
public static TableOptions<T> ColumnOrder<T>(TableOptions<T> options, string property, int order)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`order` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Ignore&lt;T&gt;(TableOptions&lt;T&gt;, String)**

```csharp
public static TableOptions<T> Ignore<T>(TableOptions<T> options, string property)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **ColumnHeader&lt;T&gt;(TableOptions&lt;T&gt;, String, String)**

```csharp
public static TableOptions<T> ColumnHeader<T>(TableOptions<T> options, string property, string newHeader)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`newHeader` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Format&lt;T&gt;(TableOptions&lt;T&gt;, String, Func&lt;T, Object, TextValue&gt;)**

```csharp
public static TableOptions<T> Format<T>(TableOptions<T> options, string property, Func<T, object, TextValue> format)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`format` Func&lt;T, Object, TextValue&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **PrintHeader&lt;T&gt;(TableOptions&lt;T&gt;, Boolean)**

```csharp
public static TableOptions<T> PrintHeader<T>(TableOptions<T> options, bool print)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`print` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **PrintFirstLineSeparator&lt;T&gt;(TableOptions&lt;T&gt;, Boolean)**

```csharp
public static TableOptions<T> PrintFirstLineSeparator<T>(TableOptions<T> options, bool print)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`print` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **PrintLastLineSeparator&lt;T&gt;(TableOptions&lt;T&gt;, Boolean)**

```csharp
public static TableOptions<T> PrintLastLineSeparator<T>(TableOptions<T> options, bool print)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`print` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **SetColumn&lt;T&gt;(TableOptions&lt;T&gt;, String, Func&lt;T, Object&gt;)**

```csharp
public static TableOptions<T> SetColumn<T>(TableOptions<T> options, string property, Func<T, object> getValue)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`getValue` Func&lt;T, Object&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Cast&lt;T&gt;(TableOptions)**

```csharp
public static TableOptions<T> Cast<T>(TableOptions options)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` [TableOptions](./albatross/text/table/tableoptions)<br>

#### Returns

TableOptions&lt;T&gt;<br>

---

[`< Back`](../../../)
