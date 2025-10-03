using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestArrayPropertyWithDateOnly : BaseHandler<TestUseCaseOptions> {
		public TestArrayPropertyWithDateOnly(IOptions<TestUseCaseOptions> options) : base(options) {
		}
		public override int Invoke(InvocationContext context) {
			var faker = new Faker();
			var item = Enumerable.Range(1, 5).Select(x => Contact.Random(faker)).ToArray();
			this.writer.CliPrint(item, "arrayproperty(value, dob)");
			return 0;
		}
	}
}