using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestElementPropertyWithDateOnly : BaseHandler<TestUseCaseOptions> {
		public TestElementPropertyWithDateOnly(IOptions<TestUseCaseOptions> options) : base(options) {
		}
		public override int Invoke(InvocationContext context) {
			var faker = new Faker() {
				Random = new Bogus.Randomizer(12345)
			};
			var item = Enumerable.Range(1, 5).Select(x => Contact.Create(faker)).ToArray();
			var format = "collection_property(value, dob)";
			this.writer.WriteLine("Format: " + format);
			this.writer.CliPrint(item, format);
			return 0;
		}
	}
}