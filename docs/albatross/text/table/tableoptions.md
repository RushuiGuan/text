[`< Back`](../../../)

---

# TableOptions

Namespace: Albatross.Text.Table

```csharp
public class TableOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableOptions](./albatross/text/table/tableoptions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Type**

```csharp
public Type Type { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

### **Headers**

```csharp
public String[] Headers { get; }
```

#### Property Value

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PrintHeader**

```csharp
public bool PrintHeader { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **PrintFirstLineSeparator**

```csharp
public bool PrintFirstLineSeparator { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **PrintLastLineSeparator**

```csharp
public bool PrintLastLineSeparator { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ColumnOptions**

```csharp
public IEnumerable<TableColumnOption> ColumnOptions { get; }
```

#### Property Value

[IEnumerable&lt;TableColumnOption&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

## Constructors

### **TableOptions(Type)**

```csharp
public TableOptions(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

## Methods

### **GetValue(Object)**

```csharp
public TextValue[] GetValue(object item)
```

#### Parameters

`item` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[TextValue[]](./albatross/text/table/textvalue)<br>

### **GetRequiredColumn(String)**

```csharp
public TableColumnOption GetRequiredColumn(string property)
```

#### Parameters

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[TableColumnOption](./albatross/text/table/tablecolumnoption)<br>

### **GetColumn(String)**

```csharp
public TableColumnOption GetColumn(string property)
```

#### Parameters

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[TableColumnOption](./albatross/text/table/tablecolumnoption)<br>

### **Remove(String)**

```csharp
public TableOptions Remove(string property)
```

#### Parameters

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[TableOptions](./albatross/text/table/tableoptions)<br>

### **SetColumn(String, Func&lt;Object, Object&gt;)**

```csharp
public void SetColumn(string property, Func<object, object> getValue)
```

#### Parameters

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`getValue` [Func&lt;Object, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **Build()**

```csharp
public IEnumerable<TableColumnOption> Build()
```

#### Returns

[IEnumerable&lt;TableColumnOption&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **DefaultFormat(Object)**

```csharp
public static string DefaultFormat(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

---

[`< Back`](../../../)
