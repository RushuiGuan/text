[`< Back`](../../../)

---

# StringTable

Namespace: Albatross.Text.Table

```csharp
public class StringTable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StringTable](./albatross/text/table/stringtable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Columns**

```csharp
public Column[] Columns { get; }
```

#### Property Value

[Column[]](./albatross/text/table/stringtable/column)<br>

### **Rows**

```csharp
public IEnumerable<Row> Rows { get; }
```

#### Property Value

[IEnumerable&lt;Row&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

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

### **TotalWidth**

```csharp
public int TotalWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **StringTable(IEnumerable&lt;String&gt;)**

```csharp
public StringTable(IEnumerable<string> headers)
```

#### Parameters

`headers` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **StringTable(IEnumerable, TableOptions)**

```csharp
public StringTable(IEnumerable items, TableOptions options)
```

#### Parameters

`items` [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>

`options` [TableOptions](./albatross/text/table/tableoptions)<br>

## Methods

### **AdjustColumnWidth(Int32)**

```csharp
public void AdjustColumnWidth(int maxWidth)
```

#### Parameters

`maxWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AddRow(IEnumerable&lt;String&gt;)**

```csharp
public void AddRow(IEnumerable<string> values)
```

#### Parameters

`values` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **AddRow(IEnumerable&lt;TextValue&gt;)**

```csharp
public void AddRow(IEnumerable<TextValue> values)
```

#### Parameters

`values` [IEnumerable&lt;TextValue&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **Print(TextWriter, Nullable&lt;Boolean&gt;, Nullable&lt;Boolean&gt;, Nullable&lt;Boolean&gt;)**

```csharp
public void Print(TextWriter writer, Nullable<bool> printHeader, Nullable<bool> printFirstLineSeparator, Nullable<bool> printLastLineSeparator)
```

#### Parameters

`writer` [TextWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.textwriter)<br>

`printHeader` [Nullable&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`printFirstLineSeparator` [Nullable&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

`printLastLineSeparator` [Nullable&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FilterColumns(String[])**

```csharp
public StringTable FilterColumns(String[] columns)
```

#### Parameters

`columns` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **FilterRows(String, Func&lt;String, Boolean&gt;)**

```csharp
public StringTable FilterRows(string column, Func<string, bool> predicate)
```

#### Parameters

`column` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`predicate` [Func&lt;String, Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

---

[`< Back`](../../../)
