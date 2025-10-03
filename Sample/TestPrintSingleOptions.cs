using Albatross.CommandLine;

namespace Sample {
	[Verb("single contact", typeof(TestPrintSingle<Contact>), Description = "Print a contact")]
	[Verb("single address", typeof(TestPrintSingle<Address>), Description = "Print an addresse")]
	public class TestPrintSingleOptions {
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}
}