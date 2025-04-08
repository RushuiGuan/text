﻿using Albatross.Text.Table;
using FluentAssertions;
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
			var options = new TableOptionBuilder<TestClass>().SetColumnsByReflection().Build();
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M };
			var writer = new StringWriter();
			new[] { obj }.MarkdownTable(writer, options);
			writer.ToString().Should().Be("Id|Name|Value\r\n-|-|-\r\n1|name|1\r\n");
		}

		[Fact]
		public void TestStringTablePrinting() {
			var options = new TableOptionBuilder<TestClass>().SetColumnsByReflection().Build();
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M };
			var table = new[] { obj }.StringTable(options);
			Assert.Equal(2, table.Columns[0].MaxWidth);
			Assert.Equal(4, table.Columns[1].MaxWidth);
			Assert.Equal(5, table.Columns[2].MaxWidth);
			var writer = new StringWriter();
			table.Print(writer);
			writer.ToString().Should().Be("Id Name Value\r\n-------------\r\n1  name 1    \r\n-------------\r\n");
		}


		[Fact]
		public void TestStringTablePrintingWithMismatchValueLength() {
			var options = new TableOptionBuilder<TestClass>()
				.SetColumnsByReflection()
				.Format(x => x.Markdown, (e, v) => new TextValue("[Google](https://www.google.com)", "Google".Length))
				.ColumnOrder(x => x.Markdown, -1)
				.Build();
			var obj = new TestClass { Id = 1, Name = "name", Value = 1.0M, Markdown = "Google" };
			var table = new[] { obj }.StringTable(options).SetColumn(x => x.Name == "Markdown", x => { x.NeverTruncate = true; });
			Assert.Equal(8, table.Columns[0].MaxWidth);
			Assert.Equal(2, table.Columns[1].MaxWidth);
			Assert.Equal(4, table.Columns[2].MaxWidth);
			Assert.Equal(5, table.Columns[3].MaxWidth);
			var writer = new StringWriter();
			table.Print(writer);
			writer.ToString().Should().Be("Markdown Id Name Value\r\n----------------------\r\n[Google](https://www.google.com)   1  name 1    \r\n----------------------\r\n");
		}
	}
}
