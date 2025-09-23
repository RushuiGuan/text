using Albatross.Text.CliFormat;
using Albatross.CommandLine;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("print", typeof(TestPrint), Description = "Test command for demonstration purposes")]
	public class TestPrintOptions {
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}

	public class TestPrint : BaseHandler<TestPrintOptions> {
		public TestPrint(IOptions<TestPrintOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = fixture.CreateMany<Contact>(20);
			contacts.CliPrint(options.Format);
			return 0;
		}
	}
}