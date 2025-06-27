using Albatross.CommandLine;
using Albatross.Text.Table;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test print-columns", typeof(TestPrintColumns), Description = "Test command for demonstration purposes")]
	public class TestNoHeaderOptions {
		[Option("c")]
		public string? Columns { get; set; }
	}

	public class TestPrintColumns : BaseHandler<TestNoHeaderOptions> {
		public TestPrintColumns(IOptions<TestNoHeaderOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = fixture.CreateMany<Contact>(5);
			contacts.StringTable().PrintConsole(this.options.Columns);
			return 0;
		}
	}
}