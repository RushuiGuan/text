using Albatross.CommandLine;

namespace Sample {
	[Verb("collection contact", typeof(TestPrintCollection<Contact>), Description = "Print a list of contacts")]
	[Verb("collection address", typeof(TestPrintCollection<Address>), Description = "Print a list of addresses")]
	public class TestPrintCollectionOptions {
		[Option("c", Description = "Item count", DefaultToInitializer = true, Required = false)]
		public int Count { get; set; } = 5;
		
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}
}