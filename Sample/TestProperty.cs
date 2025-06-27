using Albatross.CommandLine;
using Albatross.Text.Table;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test property", typeof(TestProperty), Description = "Test command for demonstration purposes")]
	public class TestPropertyOptions {
		public string? Format { get; set; }
	}

	public class TestProperty : BaseHandler<TestPropertyOptions> {
		public TestProperty(IOptions<TestPropertyOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = fixture.CreateMany<Contact>(20);
			contacts.First().PropertyTable().PrintConsole();
			return 0;
		}
	}
}