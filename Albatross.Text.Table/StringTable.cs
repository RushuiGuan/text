using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text.Table {
	public class StringTable {
		public record class Column {
			public Column(string name, int index) {
				Name = name;
				Index = index;
				SetMaxTextWidth(name.Length);
			}
			public int Index { get; }
			public string Name { get; }
			/// <summary>
			/// The maximum width of the text for this column
			/// </summary>
			public int MaxTextWidth { get; private set; }

			public void SetMaxTextWidth(int width) {
				if (width > MaxTextWidth) {
					MaxTextWidth = width;
					DisplayWidth = width;
				}
			}
			/// <summary>
			/// The minimum width of this column.  If set to 0, the column will be hidden
			/// </summary>
			public int MinWidth { get; set; } = int.MaxValue;
			public int DisplayWidth { get; internal set; }
			public int ActualWidth => DisplayWidth == 0 ? 0 : DisplayWidth + 1;
			public bool AlignRight { get; set; }
			public string GetText(TextValue value) {
				return value.GetText(DisplayWidth, AlignRight);
			}
		}
		public class Row {
			public TextValue[] Values { get; }
			public Row(params TextValue[] values) {
				Values = values;
			}
		}
		Column[] columns;
		List<Row> rows = new List<Row>();

		public Column[] Columns => columns;
		public IEnumerable<Row> Rows => rows;

		public StringTable(params IEnumerable<string> headers) {
			columns = headers.Select((x, index) => new Column(x, index)).ToArray();
		}

		public int TotalWidth => Columns.Sum(x => x.ActualWidth) - 1;

		public void AdjustColumnWidth(int maxWidth) {
			var overflow = TotalWidth - maxWidth;
			if (overflow > 0) {
				for (int i = Columns.Length - 1; i >= 0; i--) {
					var column = Columns[i];
					// if the MinWidth of the column is set, reduce the DisplayWidth as much as possible
					if (column.MinWidth < column.MaxTextWidth) {
						column.DisplayWidth = Math.Max(column.MinWidth, column.DisplayWidth - overflow);
						overflow = TotalWidth - maxWidth;
						// when a column width is set to 0, the reduced width could be width + 1 therefore
						// could lead to a negative overflow
						if (overflow <= 0) {
							return;
						}
					}
				}
				for (int i = columns.Length - 1; i >= 0; i--) {
					var column = Columns[i];
					// this time, reduce the DisplayWidth regardless of the MinWidth
					column.DisplayWidth = Math.Max(0, column.DisplayWidth - overflow);
					overflow = TotalWidth - maxWidth;
					if (overflow <= 0) {
						return;
					}
				}
			}
		}

		public void Add(params IEnumerable<string> values) => this.Add(values.Select(x => new TextValue(x)));

		public void Add(params IEnumerable<TextValue> values) {
			var array = values.ToArray();
			if (array.Length != columns.Length) {
				throw new ArgumentException($"Table is expecting rows with {columns.Length} columns");
			}
			rows.Add(new Row(array));
			for (int i = 0; i < columns.Length; i++) {
				columns[i].SetMaxTextWidth(array[i].TextWidth);
			}
		}

		public void Print(TextWriter writer) {
			var visibleColumns = this.Columns.Where(x => x.DisplayWidth > 0).ToArray();
			var header = string.Join(" ", visibleColumns.Select(x => x.GetText(new TextValue(x.Name))));
			writer.WriteLine(header);
			writer.WriteLine("-".PadRight(header.Length, '-'));
			string? body = null;
			foreach (var row in Rows) {
				body = string.Join(" ", visibleColumns.Select(x => x.GetText(row.Values[x.Index])));
				writer.WriteLine(body);
			}
			if (!string.IsNullOrEmpty(body)) {
				writer.WriteLine("-".PadRight(header.Length, '-'));
			}
		}

		public StringTable Filter(string? column, Func<string, bool> predicate) {
			int? columnIndex = null;
			if (!string.IsNullOrEmpty(column)) {
				var selected = columns.Where(x => x.Name == column).FirstOrDefault() ?? throw new ArgumentException($"{column} is not a valid colume");
				columnIndex = selected.Index;
			}
			var stringTable = new StringTable();
			stringTable.columns = columns.Select(x => x with { }).ToArray();
			foreach (var row in Rows) {
				if (Filter(columnIndex, row, predicate)) {
					stringTable.rows.Add(row);
				}
			}
			return stringTable;
		}

		internal bool Filter(int? columnIndex, Row row, Func<string, bool> predicate) {
			TextValue[] array;
			if (columnIndex.HasValue) {
				array = [row.Values[columnIndex.Value]];
			} else {
				array = row.Values;
			}
			foreach (var value in array) {
				if (predicate(value.Text)) {
					return true;
				}
			}
			return false;
		}
	}
}