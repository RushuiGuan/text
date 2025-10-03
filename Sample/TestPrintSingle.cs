using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	public class TestPrintSingle<T> : BaseHandler<TestPrintSingleOptions> where T : notnull {
		public TestPrintSingle(IOptions<TestPrintSingleOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Bogus.Faker("en") {
				Random = new Randomizer(12345)
			};
			if (typeof(T) == typeof(Contact)) {
				var item = Contact.Create(faker);
				this.writer.CliPrint(item, options.Format);
			} else if (typeof(T) == typeof(Address)) {
				var item = Address.Create(faker);
				this.writer.CliPrint(item, options.Format);
			}
			return 0;
		}
	}
}