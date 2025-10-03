using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestJsonPointerUserCase : BaseHandler<TestUseCaseOptions> {
		public TestJsonPointerUserCase(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Faker() {
				Random = new Bogus.Randomizer(12345)
			};
			var items = Enumerable.Range(0, 5).Select(x => Address.Create(faker)).ToArray();
			var format = "jsonpointer(value, /0/street)";
			this.writer.WriteLine("Format: " + format);
			this.writer.CliPrint(items, format);
			return 0;
		}
	}
}