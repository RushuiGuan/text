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
			// Use a fixed seed for deterministic data generation
			var faker = new Faker("en") { Random = new Randomizer(12345) };

			IEnumerable<T> items;
			if (typeof(T) == typeof(Contact)) {
				items = Enumerable.Range(1, options.Count).Select(_ => Contact.Create(faker)).Cast<T>().ToArray();
			} else if (typeof(T) == typeof(Address)) {
				items = Enumerable.Range(1, options.Count).Select(_ => Address.Create(faker)).Cast<T>().ToArray();
			} else {
				throw new NotSupportedException($"Type {typeof(T).FullName} is not supported");
			}
			this.writer.CliPrint(items, options.Format);
			return 0;
		}
	}
}