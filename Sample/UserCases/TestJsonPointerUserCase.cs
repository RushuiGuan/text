using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestJsonPointerUserCase : BaseHandler<TestUseCaseOptions> {
		public TestJsonPointerUserCase(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var items = fixture.CreateMany<Address>(5);
			this.writer.CliPrint(items, "jsonpointer(value, /0/line1)");
			return 0;
		}
	}
}