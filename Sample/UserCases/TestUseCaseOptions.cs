using Albatross.CommandLine;

namespace Sample.UserCases {
	[Verb("use-case json", typeof(TestJsonUserCase), Description = "Test json command")]
	[Verb("use-case json-pointer", typeof(TestJsonPointerUserCase), Description = "Test json command")]
	[Verb("use-case string-array-property", typeof(TestElementPropertyWithStringValue), Description = "When a property is a string array, just print the strings directly")]
	[Verb("use-case int-array-property", typeof(TestElementPropertyWithInt), Description = "When a property is a primitive, just print the value directly")]
	[Verb("use-case dateonly-array-property", typeof(TestElementPropertyWithDateOnly), Description = "When a property is a primitive, just print the value directly")]
	[Verb("use-case object-array-property", typeof(TestElementPropertyWithObjects), Description = "Using the property command, the system will print a table if the property is a colleciton")]
	[Verb("use-case dynamic-single-property", typeof(TestDynamicSingleProperty), Description = "Using the property command, the system will print a single value if the property is not a colleciton")]
	[Verb("use-case table-array-property", typeof(TestTableArrayProperty), Description = "Table is the default command for collection value.  The command itself allows column selection")]
	[Verb("use-case dictionary", typeof(TestDictionary), Description = "Should print key value pairs")]
	public class TestUseCaseOptions {
	}
}