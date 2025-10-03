using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestJsonUserCase : BaseHandler<TestUseCaseOptions> {
		public TestJsonUserCase(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Faker();
			var items = Enumerable.Range(0, 5).Select(x => Address.Random(faker)).ToArray();
			this.writer.CliPrint(items, "json(value)");
			return 0;
		}
	}
}