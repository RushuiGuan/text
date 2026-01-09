using Albatross.Text.CliFormat;
using Bogus;
using System.Collections.Generic;
using Xunit;

namespace Albatross.Text.Test {
	public class TestCliFormat {
		public class Sample {
			public int Id { get; set; }
			public required string Name { get; set; }
		}

		public void TestPrintWithExpression(string expression) {
			var faker = new Faker<Sample>();
			var items = faker.Generate(10);
			var expression = CliFormat.Extensions.BuildCustomParser()
		}
	}
}