using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("single contact", typeof(TestPrintSingle<Contact>), Description = "Print a contact")]
	[Verb("single address", typeof(TestPrintSingle<Address>), Description = "Print an addresse")]
	public class TestPrintSingleOptions {
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}

	public class TestPrintSingle<T> : BaseHandler<TestPrintSingleOptions> where T : notnull {
		public TestPrintSingle(IOptions<TestPrintSingleOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var item = fixture.Create<T>();
			this.writer.CliPrint(item, options.Format);
			return 0;
		}
	}
}