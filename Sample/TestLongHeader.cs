using Albatross.CommandLine;
using Albatross.Text.Table;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("test long-header", typeof(TestLongHeader), Description = "Testing situation where header is longer than cell text")]
	public class TestLongHeaderOptions {
	}

	public class TestLongHeader : BaseHandler<TestLongHeaderOptions> {
		public TestLongHeader(IOptions<TestLongHeaderOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var contacts = fixture.CreateMany<Contact>(5);
			var printOptions = new TableOptionBuilder<Contact>()
				.SetColumnsByReflection()
				.ColumnHeader("Age", () => "Age (Years)").Ignore(x => x.Address).Ignore(x => x.Email).Ignore(x => x.Phone)
				.ColumnOrder("Name", () => 100)
				.Build();
			contacts.StringTable(printOptions).PrintConsole();
			return 0;
		}
	}
}