using Albatross.Text.Table;
using FluentAssertions;
using Xunit;

namespace Albatross.Text.Test {
	public class TestStringTableFilter {
		[Theory]
		[InlineData(true, null, "b", FilterMode.Contains, "123", "abc")]
		[InlineData(true, null, "a", FilterMode.StartsWith, "123", "abc")]
		[InlineData(true, null, "c", FilterMode.EndsWith, "123", "abc")]

		[InlineData(true, null, "2", FilterMode.Contains, "123", "abc")]
		[InlineData(true, null, "1", FilterMode.StartsWith, "123", "abc")]
		[InlineData(true, null, "3", FilterMode.EndsWith, "123", "abc")]

		[InlineData(true, 0, "2", FilterMode.Contains, "123", "abc")]
		[InlineData(true, 0, "1", FilterMode.StartsWith, "123", "abc")]
		[InlineData(true, 0, "3", FilterMode.EndsWith, "123", "abc")]

		[InlineData(true, 1, "b", FilterMode.Contains, "123", "abc")]
		[InlineData(true, 1, "a", FilterMode.StartsWith, "123", "abc")]
		[InlineData(true, 1, "c", FilterMode.EndsWith, "123", "abc")]

		[InlineData(false, 1, "2", FilterMode.Contains, "123", "abc")]
		[InlineData(false, 1, "1", FilterMode.StartsWith, "123", "abc")]
		[InlineData(false, 1, "3", FilterMode.EndsWith, "123", "abc")]

		[InlineData(false, 0, "b", FilterMode.Contains, "123", "abc")]
		[InlineData(false, 0, "a", FilterMode.StartsWith, "123", "abc")]
		[InlineData(false, 0, "c", FilterMode.EndsWith, "123", "abc")]

		[InlineData(true, 1, "C", FilterMode.EndsWith, "123", "abc")]
		[InlineData(false, 1, "C", FilterMode.EndsWith | FilterMode.CaseSensitive, "123", "abc")]
		public void TestFilter(bool expected, int? columnIndex, string text, FilterMode mode, params string[] items) {
			var row = new StringTable.Row(items);
			new StringTable().Filter(columnIndex, row, text, mode).Should().Be(expected);
		}

		[Fact]
		public void TestTableFilter() {
			var table = new StringTable("", "");
			table.Add("123", "abc");
			table.Add("456", "def");
			table.Add("456", "DEF");
			table.Add("789", "ghi");
			var filteredTable = table.Filter(null, "a", FilterMode.Contains);
			filteredTable.Rows.Should().BeEquivalentTo([new StringTable.Row("123", "abc")]);

			filteredTable = table.Filter(null, "1", FilterMode.StartsWith);
			filteredTable.Rows.Should().BeEquivalentTo([new StringTable.Row("123", "abc")]);

			filteredTable = table.Filter(null, "3", FilterMode.EndsWith);
			filteredTable.Rows.Should().BeEquivalentTo([new StringTable.Row("123", "abc")]);

			filteredTable = table.Filter(null, "6", FilterMode.EndsWith);
			filteredTable.Rows.Should().BeEquivalentTo([new StringTable.Row("456", "def"), new StringTable.Row("456", "DEF")]);

			filteredTable = table.Filter(null, "d", FilterMode.StartsWith | FilterMode.CaseSensitive);
			filteredTable.Rows.Should().BeEquivalentTo([new StringTable.Row("456", "def")]);
		}
	}
}
