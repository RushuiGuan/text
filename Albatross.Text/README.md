# Albatross.Text
A string manipulation library

## Features
* Contains extension methods for StringBuilder class and TextWriter class
* String interpolation
[StringInterpolationExtensions](./StringInterpolationExtensions.cs) provides the functionality to manipulate string by interpolation.  As shown in the example below, it seeks out expressions that are enclosed with `${` and `}` and replace it with the value provided by the func delegate.
```csharp
[Theory]
[InlineData("abc_${b}_abc_${b}", @"{""b"":""1""}", "abc_1_abc_1")]
public void TestStringInterpolationWithoutObject(string text, string dictionary, string expected) {
	var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(dictionary) ?? new Dictionary<string, string>();
	var result = text.Interpolate(args => dict[args]);
	Assert.Equal(expected, result);
}
```
