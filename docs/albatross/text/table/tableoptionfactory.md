[`< Back`](../../../)

---

# TableOptionFactory

Namespace: Albatross.Text.Table

```csharp
public class TableOptionFactory
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableOptionFactory](./albatross/text/table/tableoptionfactory)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Instance**

```csharp
public static TableOptionFactory Instance { get; }
```

#### Property Value

[TableOptionFactory](./albatross/text/table/tableoptionfactory)<br>

## Constructors

### **TableOptionFactory()**

```csharp
public TableOptionFactory()
```

## Methods

### **Get&lt;T&gt;()**

```csharp
public TableOptions<T> Get<T>()
```

#### Type Parameters

`T`<br>

#### Returns

TableOptions&lt;T&gt;<br>

### **Get(Type)**

```csharp
public TableOptions Get(Type type)
```

#### Parameters

`type` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[TableOptions](./albatross/text/table/tableoptions)<br>

### **Register&lt;T&gt;(TableOptions&lt;T&gt;)**

```csharp
public void Register<T>(TableOptions<T> options)
```

#### Type Parameters

`T`<br>

#### Parameters

`options` TableOptions&lt;T&gt;<br>

### **Register(TableOptions)**

```csharp
public void Register(TableOptions options)
```

#### Parameters

`options` [TableOptions](./albatross/text/table/tableoptions)<br>

---

[`< Back`](../../../)
