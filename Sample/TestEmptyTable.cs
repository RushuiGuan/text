using Albatross.CommandLine;
using Albatross.Text.Table;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test empty-table", typeof(TestEmptyTable), Description = "Test command for demonstration purposes")]
	public class TestEmptyTableOptions {
		public string? Format { get; set; }
	}

	public class TestEmptyTable : BaseHandler<TestEmptyTableOptions> {
		public TestEmptyTable(IOptions<TestEmptyTableOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var contacts = new Contact[0];
			contacts.StringTable().PrintConsole();
			return 0;
		}
	}
}