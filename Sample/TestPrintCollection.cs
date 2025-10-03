using Albatross.Text.CliFormat;
using Albatross.CommandLine;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	public class TestPrintCollection<T> : BaseHandler<TestPrintCollectionOptions> {
		public TestPrintCollection(IOptions<TestPrintCollectionOptions> options) : base(options) {
		}

		public override int Invoke(InvocationContext context) {
			var faker = new Faker("en");
			IEnumerable<T> items;
			if (typeof(T) == typeof(Contact)) {
				items = Enumerable.Range(1, options.Count).Select(_ => Contact.Random(faker)).Cast<T>();
			}else if(typeof(T) == typeof(Address)) {
				items = Enumerable.Range(1, options.Count).Select(_ => Address.Random(faker)).Cast<T>();
			}else {
				throw new NotSupportedException($"Type {typeof(T).FullName} is not supported");
			}
			this.writer.CliPrint(items, options.Format);
			return 0;
		}
	}
}