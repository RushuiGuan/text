using Albatross.CommandLine;

namespace Sample.UserCases {
	[Verb("use-case json", typeof(TestJsonUserCase), Description = "Test json command")]
	[Verb("use-case jsonpointer", typeof(TestJsonPointerUserCase), Description = "Test json command")]
	[Verb("use-case string-array-property", typeof(TestArrayPropertyWithStringValue), Description = "When a property is a string array, just print the strings directly")]
	[Verb("use-case int-array-property", typeof(TestIntPropertyWithPrimitiveValue), Description = "When a property is a primitive, just print the value directly")]
	public class TestUseCaseOptions {
	}
}