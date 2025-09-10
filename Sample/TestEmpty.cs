using Albatross.Text.CliFormat;
using Albatross.CommandLine;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test empty", typeof(TestEmpty), Description = "Test command for demonstration purposes")]
	public class TestEmptyOptions {
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}

	public class TestEmpty : BaseHandler<TestEmptyOptions> {
		public TestEmpty(IOptions<TestEmptyOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = new Contact[0];
			contacts.CliPrint(options.Format);
			return 0;
		}
	}
}