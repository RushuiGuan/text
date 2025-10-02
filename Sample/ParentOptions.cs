using Albatross.CommandLine;

namespace Sample {
	[Verb("collection", Alias = ["c"], Description = "Print a list of items")]
	[Verb("single", Alias = ["s"], Description = "Print a single item")]
	[Verb("use-case", Alias = ["u"], Description = "Test user cases")]
	public class ParentOptions {
	}
}