[`< Back`](../../../)

---

# StringTableExtensions

Namespace: Albatross.Text.Table

```csharp
public static class StringTableExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StringTableExtensions](./albatross/text/table/stringtableextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **StringTable&lt;T&gt;(IEnumerable&lt;T&gt;, TableOptions)**

```csharp
public static StringTable StringTable<T>(IEnumerable<T> items, TableOptions options)
```

#### Type Parameters

`T`<br>

#### Parameters

`items` IEnumerable&lt;T&gt;<br>

`options` [TableOptions](./albatross/text/table/tableoptions)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **StringTable(IDictionary)**

```csharp
public static StringTable StringTable(IDictionary dictionary)
```

#### Parameters

`dictionary` [IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **ChangeColumn(StringTable, Func&lt;Column, Boolean&gt;, Action&lt;Column&gt;)**

```csharp
public static StringTable ChangeColumn(StringTable table, Func<Column, bool> predicate, Action<Column> action)
```

#### Parameters

`table` [StringTable](./albatross/text/table/stringtable)<br>

`predicate` [Func&lt;Column, Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

`action` [Action&lt;Column&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **MinWidth(StringTable, Func&lt;Column, Boolean&gt;, Int32)**

```csharp
public static StringTable MinWidth(StringTable table, Func<Column, bool> predicate, int minWidth)
```

#### Parameters

`table` [StringTable](./albatross/text/table/stringtable)<br>

`predicate` [Func&lt;Column, Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

`minWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **AlignRight(StringTable, Func&lt;Column, Boolean&gt;, Boolean)**

```csharp
public static StringTable AlignRight(StringTable table, Func<Column, bool> predicate, bool value)
```

#### Parameters

`table` [StringTable](./albatross/text/table/stringtable)<br>

`predicate` [Func&lt;Column, Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

`value` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **SetWidthLimit(StringTable, Int32)**

```csharp
public static StringTable SetWidthLimit(StringTable table, int width)
```

#### Parameters

`table` [StringTable](./albatross/text/table/stringtable)<br>

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[StringTable](./albatross/text/table/stringtable)<br>

### **PrintConsole(StringTable, TextWriter, Int32)**

```csharp
public static void PrintConsole(StringTable table, TextWriter writer, int maxWidth)
```

#### Parameters

`table` [StringTable](./albatross/text/table/stringtable)<br>

`writer` [TextWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.textwriter)<br>

`maxWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetConsoleWith()**

```csharp
public static int GetConsoleWith()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AlignFirst(IEnumerable&lt;StringTable&gt;)**

```csharp
public static void AlignFirst(IEnumerable<StringTable> tables)
```

#### Parameters

`tables` [IEnumerable&lt;StringTable&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **AlignAll(IEnumerable&lt;StringTable&gt;)**

```csharp
public static void AlignAll(IEnumerable<StringTable> tables)
```

#### Parameters

`tables` [IEnumerable&lt;StringTable&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

---

[`< Back`](../../../)
