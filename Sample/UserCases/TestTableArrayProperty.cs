using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestTableArrayProperty: BaseHandler<TestUseCaseOptions> {
		public TestTableArrayProperty(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Bogus.Faker();
			var item = Enumerable.Range(1, 5).Select(x => Contact.Random(faker)).ToArray();
			this.writer.CliPrint(item, "table(value, firstname, lastname)");
			return 0;
		}
	}
}