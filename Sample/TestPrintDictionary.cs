using Albatross.Text.CliFormat;
using Albatross.CommandLine;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	public class TestPrintDictionary : BaseHandler<TestPrintCollectionOptions> {
		public TestPrintDictionary(IOptions<TestPrintCollectionOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Faker("en") {
				Random = new Randomizer(12345)
			};
			var dict = new Dictionary<int, string>();
			for (int i = 0; i < options.Count; i++) {
				dict[i] = faker.Person.FullName;
			}
			this.writer.CliPrint(dict, options.Format);
			return 0;
		}
	}
}