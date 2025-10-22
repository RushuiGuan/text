[`< Back`](../../../)

---

# GenericTableOptionsExtension

Namespace: Albatross.Text.Table

```csharp
public static class GenericTableOptionsExtension
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [GenericTableOptionsExtension](./albatross/text/table/generictableoptionsextension)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Format&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, String)**

```csharp
public static TableOptions<T> Format<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda, string format)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

`format` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Format&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, Func&lt;T, Object, TextValue&gt;)**

```csharp
public static TableOptions<T> Format<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object, TextValue> format)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

`format` Func&lt;T, Object, TextValue&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **ColumnOrder&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, Int32)**

```csharp
public static TableOptions<T> ColumnOrder<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda, int order)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

`order` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **ColumnHeader&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, String)**

```csharp
public static TableOptions<T> ColumnHeader<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda, string newHeader)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

`newHeader` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Ignore&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;)**

```csharp
public static TableOptions<T> Ignore<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **SetColumn&lt;T, P&gt;(TableOptions&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, Func&lt;T, Object&gt;)**

```csharp
public static TableOptions<T> SetColumn<T, P>(TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object> getValue)
```

#### Type Parameters

`T`<br>

`P`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`lambda` Expression&lt;Func&lt;T, P&gt;&gt;<br>

`getValue` Func&lt;T, Object&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **BuildColumnsByReflection&lt;T&gt;(TableOptions&lt;T&gt;)**

```csharp
public static TableOptions<T> BuildColumnsByReflection<T>(TableOptions<T> options)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **SetColumn&lt;T&gt;(TableOptions&lt;T&gt;, String)**

```csharp
public static TableOptions<T> SetColumn<T>(TableOptions<T> options, string column)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

`column` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

TableOptions&lt;T&gt;<br>

---

[`< Back`](../../../)
