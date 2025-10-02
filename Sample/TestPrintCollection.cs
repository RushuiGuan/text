using Albatross.Text.CliFormat;
using Albatross.CommandLine;
using AutoFixture;
using Bogus;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample {
	[Verb("collection contact", typeof(TestPrintCollection<Contact>), Description = "Print a list of contacts")]
	[Verb("collection address", typeof(TestPrintCollection<Address>), Description = "Print a list of addresses")]
	public class TestPrintCollectionOptions {
		[Option("c", Description = "Item count", DefaultToInitializer = true, Required = false)]
		public int Count { get; set; } = 5;
		
		[Option("f", Description = "Output format")]
		public string? Format { get; set; }
	}

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