using Albatross.CommandLine;
using Albatross.Text.Table;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test table", typeof(TestTable), Description = "Test command for demonstration purposes")]
	public class TestTableOptions {
		public string? Format { get; set; }
	}

	public class TestTable : BaseHandler<TestTableOptions> {
		public TestTable(IOptions<TestTableOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = fixture.CreateMany<Contact>(20);
			contacts.StringTable().PrintConsole();
			return 0;
		}
	}
}