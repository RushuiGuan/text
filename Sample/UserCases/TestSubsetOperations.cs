using Albatross.CommandLine;
using Albatross.Text.CliFormat;
using Microsoft.Extensions.Options;
using System.CommandLine.Invocation;

namespace Sample.UserCases {
    public class TestSubsetOperations : BaseHandler<TestUseCaseOptions> {
        public TestSubsetOperations(IOptions<TestUseCaseOptions> options) : base(options) {
        }

        public override int Invoke(InvocationContext context) {
            var faker = new Bogus.Faker() {
                Random = new Bogus.Randomizer(12345)
            };
            var item = Enumerable.Range(1, 5).Select(x => Contact.Create(faker)).ToArray();
            var format = "table(subset(value, 1, 3), firstname, lastname)";
            this.writer.WriteLine("Format: " + format);
            this.writer.CliPrint(item, format);
            return 0;
        }
    }
}