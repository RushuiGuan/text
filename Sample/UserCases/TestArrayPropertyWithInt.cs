using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestArrayPropertyWithInt : BaseHandler<TestUseCaseOptions> {
		public TestArrayPropertyWithInt(IOptions<TestUseCaseOptions> options) : base(options) {
		}
		public override int Invoke(InvocationContext context) {
			var faker = new Faker();
			var item = Enumerable.Range(1, 5).Select(x => Contact.Random(faker)).ToArray();
			this.writer.CliPrint(item, "arrayproperty(value, age)");
			return 0;
		}
	}
}