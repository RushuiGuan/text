[`< Back`](../../../)

---

# TableColumnOption&lt;T&gt;

Namespace: Albatross.Text.Table

```csharp
public class TableColumnOption<T> : TableColumnOption
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TableColumnOption](./albatross/text/table/tablecolumnoption) → [TableColumnOption&lt;T&gt;](./albatross/text/table/tablecolumnoption-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Property**

```csharp
public string Property { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetValueDelegate**

```csharp
public Func<object, object> GetValueDelegate { get; set; }
```

#### Property Value

[Func&lt;Object, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **Formatter**

```csharp
public Func<object, object, TextValue> Formatter { get; set; }
```

#### Property Value

[Func&lt;Object, Object, TextValue&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-3)<br>

### **Header**

```csharp
public string Header { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Order**

```csharp
public int Order { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TableColumnOption(String, Func&lt;T, Object&gt;, Func&lt;T, Object, TextValue&gt;)**

#### Caution

Constructors of types with required members are not supported in this version of your compiler.

---

```csharp
public TableColumnOption(string property, Func<T, object> getValueDelegate, Func<T, object, TextValue> formatter)
```

#### Parameters

`property` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`getValueDelegate` Func&lt;T, Object&gt;<br>

`formatter` Func&lt;T, Object, TextValue&gt;<br>

## Methods

### **NewGetValueDelegate(Func&lt;T, Object&gt;)**

```csharp
public TableColumnOption NewGetValueDelegate(Func<T, object> getValueDelegate)
```

#### Parameters

`getValueDelegate` Func&lt;T, Object&gt;<br>

#### Returns

[TableColumnOption](./albatross/text/table/tablecolumnoption)<br>

### **NewFormatter(Func&lt;T, Object, TextValue&gt;)**

```csharp
public TableColumnOption NewFormatter(Func<T, object, TextValue> formatter)
```

#### Parameters

`formatter` Func&lt;T, Object, TextValue&gt;<br>

#### Returns

[TableColumnOption](./albatross/text/table/tablecolumnoption)<br>

---

[`< Back`](../../../)
