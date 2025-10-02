using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using AutoFixture;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestJsonUserCase : BaseHandler<TestUseCaseOptions> {
		public TestJsonUserCase(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var fixture = new Fixture();
			var items = fixture.CreateMany<Address>(5);
			this.writer.CliPrint(items, "json(value)");
			return 0;
		}
	}
}