using Albatross.Testing;
using Albatross.Text.Table;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Albatross.Text.Test {
	public class TestStringTable {
		public class TestClass {
			public int Id { get; set; }
			public string? Name { get; set; }
			public decimal? Value { get; set; }
			public string? Markdown { get; set; }
		}
		[Fact]
		public void TestMarkdownTableConversion() {
			var options = new TableOptions<TestClass>()
				.SetColumn(x => x.Id)
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Value);
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M };
			var writer = new StringWriter();
			new[] { obj }.MarkdownTable(writer, options);
			writer.ToString().NormalizeLineEnding().Should().Be("Id|Name|Value\n-|-|-\n1|name|1\n");
		}

		[Fact]
		public void TestStringTablePrinting() {
			var options = new TableOptions<TestClass>()
				.SetColumn(x => x.Id)
				.SetColumn(x => x.Name)
				.SetColumn(x => x.Value);
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M };
			var table = new[] { obj }.StringTable(options);
			Assert.Equal(2, table.Columns[0].MaxTextWidth);
			Assert.Equal(4, table.Columns[1].MaxTextWidth);
			Assert.Equal(5, table.Columns[2].MaxTextWidth);
			var writer = new StringWriter();
			table.Print(writer);
			writer.ToString().NormalizeLineEnding()
				.Should().Be("Id Name Value\n-------------\n1  name 1    \n-------------\n");
		}


		[Fact]
		public void TestStringTablePrintingWithMismatchValueLength() {
			var options = new TableOptions<TestClass>()
				.BuildColumnsByReflection().Cast<TestClass>()
				.Format(x => x.Markdown, (e, v) => new TextValue("[Google](https://www.google.com)", "Google".Length, (_, size) => ""))
				.ColumnOrder(x => x.Markdown, -1);
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M, Markdown = "Google" };
			var table = new[] { obj }.StringTable(options);
			Assert.Equal(8, table.Columns[0].MaxTextWidth);
			Assert.Equal(2, table.Columns[1].MaxTextWidth);
			Assert.Equal(4, table.Columns[2].MaxTextWidth);
			Assert.Equal(5, table.Columns[3].MaxTextWidth);
			var writer = new StringWriter();
			table.Print(writer);
			writer.ToString().NormalizeLineEnding().Should().Be("Markdown Id Name Value\n----------------------\n[Google](https://www.google.com)   1  name 1    \n----------------------\n");
		}

		[Fact]
		public void TestDictionary() {
			var dict = new Dictionary<string, string> {
				{ "Key1", "Value1" },
				{ "Key2", "Value2" },
				{ "Key3", "Value3" }
			};
			var writer = new StringWriter();
			dict.StringTable().Print(writer);
			writer.ToString().NormalizeLineEnding().Should().Be("Key  Value \n-----------\nKey1 Value1\nKey2 Value2\nKey3 Value3\n-----------\n");
		}
		[Fact]
		public void TestStringArray() {
			var array  = new string[]{
				"Value1",
				"Value2",
				"Value3"
			};
			var writer = new StringWriter();
			array.StringTable().Print(writer);
			writer.ToString().NormalizeLineEnding().Should().Be("Value1\nValue2\nValue3\n");
		}
	}
}
