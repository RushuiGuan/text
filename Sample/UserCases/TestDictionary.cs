using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
	public class TestDictionary: BaseHandler<TestUseCaseOptions> {
		public TestDictionary(IOptions<TestUseCaseOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Bogus.Faker();
			var dict = new Dictionary<int, string>();
			for(var i = 0; i< 5; i++) {
				dict[i] = faker.Person.FullName;
			}
			this.writer.CliPrint(dict, null);
			return 0;
		}
	}
}