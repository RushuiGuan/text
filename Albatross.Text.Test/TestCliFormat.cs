using Albatross.Testing;
using Albatross.Text.CliFormat;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Albatross.Text.Test {
	public class TestCliFormat {
		public class Sample {
			public int Id { get; set; }
			public string Name { get; set; } = string.Empty;
		}

		static readonly List<Sample> TestItems = new List<Sample> {
			new Sample { Id = 1, Name = "aaaa" },
			new Sample { Id = 2, Name = "bbbb" },
			new Sample { Id = 3, Name = "cccc" },
			new Sample { Id = 4, Name = "dddd" },
		};

		[Theory]
		[InlineData("first(value)", "Key  Value\n----------\nId   1    \nName aaaa \n----------")]
		[InlineData("subset(value, 0, 3)", "Id Name\n-------\n1  aaaa\n2  bbbb\n3  cccc\n-------")]
		public void TestPrintWithExpression(string format, string expected) {
			var writer = new StringWriter();
			writer.CliPrint(TestItems, format);
			writer.ToString().NormalizeLineEnding().Should().Be(expected);
		}
	}
}