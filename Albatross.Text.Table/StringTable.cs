using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text.Table {
	/// <summary>
	/// Renders collections as formatted text tables with automatic column width adjustment, text truncation, and alignment.
	/// </summary>
	public class StringTable {
		/// <summary>
		/// Represents a table column with width management and text alignment capabilities.
		/// </summary>
		public record class Column {
			public Column(string name, int index) {
				Name = name;
				Index = index;
				SetMaxTextWidth(name.Length);
			}

			/// <summary>
			/// The zero-based position of this column in the table.
			/// </summary>
			public int Index { get; }

			/// <summary>
			/// The column header name.
			/// </summary>
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
			/// The minimum width of this column. If set to 0, the column will be hidden.
			/// </summary>
			public int MinWidth { get; set; } = int.MaxValue;

			/// <summary>
			/// The current display width after any width adjustments.
			/// </summary>
			public int DisplayWidth { get; internal set; }

			/// <summary>
			/// The actual width including spacing (DisplayWidth + 1 for separator, or 0 if hidden).
			/// </summary>
			public int ActualWidth => DisplayWidth == 0 ? 0 : DisplayWidth + 1;

			/// <summary>
			/// Whether to right-align text in this column. Default is left-aligned.
			/// </summary>
			public bool AlignRight { get; set; }

			/// <summary>
			/// Formats a TextValue for display in this column, applying truncation or padding as needed.
			/// </summary>
			public string GetText(TextValue value) {
				return value.GetText(DisplayWidth, AlignRight);
			}
		}

		/// <summary>
		/// Represents a single data row containing values for each column.
		/// </summary>
		public class Row {
			/// <summary>
			/// The cell values in column order.
			/// </summary>
			public TextValue[] Values { get; }

			public Row(params TextValue[] values) {
				Values = values;
			}
		}

		Column[] columns;
		List<Row> rows = new List<Row>();

		/// <summary>
		/// The table's column definitions.
		/// </summary>
		public Column[] Columns => columns;

		/// <summary>
		/// The data rows in the table.
		/// </summary>
		public IEnumerable<Row> Rows => rows;

		/// <summary>
		/// Whether to print column headers. Default is true.
		/// </summary>
		public bool PrintHeader { get; init; } = true;

		/// <summary>
		/// Whether to print a separator line after the header. Default is true.
		/// </summary>
		public bool PrintFirstLineSeparator { get; init; } = true;

		/// <summary>
		/// Whether to print a separator line after the last row. Default is true.
		/// </summary>
		public bool PrintLastLineSeparator { get; init; } = true;

		/// <summary>
		/// Creates a table with the specified column headers.
		/// </summary>
		public StringTable(params IEnumerable<string> headers) {
			columns = headers.Select((x, index) => new Column(x, index)).ToArray();
		}

		/// <summary>
		/// Creates a table from a collection using the provided configuration.
		/// </summary>
		public StringTable(IEnumerable items, TableOptions options) : this(options.Build().Select(x => x.Header)) {
			foreach (var item in items) {
				this.AddRow(options.GetValue(item));
			}
			PrintHeader = options.PrintHeader;
			PrintFirstLineSeparator = options.PrintFirstLineSeparator;
			PrintLastLineSeparator = options.PrintLastLineSeparator;
		}

		/// <summary>
		/// The total width of all visible columns including separators.
		/// </summary>
		public int TotalWidth => Columns.Sum(x => x.ActualWidth) - 1;

		/// <summary>
		/// Reduces column widths to fit within the specified maximum width, respecting MinWidth constraints.
		/// </summary>
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

		/// <summary>
		/// Adds a row with string values.
		/// </summary>
		public void AddRow(params IEnumerable<string> values) => this.AddRow(values.Select(x => new TextValue(x)));

		/// <summary>
		/// Adds a row with TextValue entries, updating column width tracking.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when the value count doesn't match the column count.</exception>
		public void AddRow(params IEnumerable<TextValue> values) {
			var array = values.ToArray();
			if (array.Length != columns.Length) {
				throw new ArgumentException($"Table is expecting rows with {columns.Length} columns");
			}
			rows.Add(new Row(array));
			for (int i = 0; i < columns.Length; i++) {
				columns[i].SetMaxTextWidth(array[i].TextWidth);
			}
		}

		/// <summary>
		/// Outputs the table to a TextWriter with headers, separators, and formatted data rows.
		/// </summary>
		public void Print(TextWriter writer, bool? printHeader = null, bool? printFirstLineSeparator = null, bool? printLastLineSeparator = null) {
			var visibleColumns = this.Columns.Where(x => x.DisplayWidth > 0).ToArray();
			var header = string.Join(" ", visibleColumns.Select(x => x.GetText(new TextValue(x.Name))));
			if (printHeader ?? this.PrintHeader) {
				writer.WriteLine(header);
			}
			if (printFirstLineSeparator ?? this.PrintFirstLineSeparator) {
				writer.WriteLine("-".PadRight(header.Length, '-'));
			}
			string? body = null;
			foreach (var row in Rows) {
				body = string.Join(" ", visibleColumns.Select(x => x.GetText(row.Values[x.Index])));
				writer.WriteLine(body);
			}
			if (printLastLineSeparator ?? this.PrintLastLineSeparator) {
				writer.WriteLine("-".PadRight(header.Length, '-'));
			}
		}

		/// <summary>
		/// Creates a new table with only the specified columns.
		/// </summary>
		/// <param name="columns">Column names to include. If empty, returns the original table.</param>
		public StringTable FilterColumns(params string[] columns) {
			if (columns.Length == 0) {
				return this;
			}
			var stringTable = new StringTable();
			var selectedColumns = this.columns
				.Where(x => columns.Contains(x.Name, StringComparer.InvariantCulture))
				.Select(x => x with { })
				.OrderBy(x => x.Index).ToArray();

			stringTable.columns = selectedColumns.Select((x, index) => new Column(x.Name, index)).ToArray();

			foreach (var row in Rows) {
				var values = selectedColumns.Select(x => row.Values[x.Index]).ToArray();
				stringTable.AddRow(values);
			}
			return stringTable;
		}

		/// <summary>
		/// Creates a new table containing only rows that match the predicate.
		/// </summary>
		/// <param name="column">The column to test, or null to test all columns.</param>
		/// <param name="predicate">Returns true for rows to include.</param>
		/// <exception cref="ArgumentException">Thrown when the specified column doesn't exist.</exception>
		public StringTable FilterRows(string? column, Func<string, bool> predicate) {
			int? columnIndex = null;
			if (!string.IsNullOrEmpty(column)) {
				var selected = columns.FirstOrDefault(x => x.Name == column) ?? throw new ArgumentException($"{column} is not a valid column");
				columnIndex = selected.Index;
			}
			var stringTable = new StringTable();
			stringTable.columns = columns.Select(x => x with { }).ToArray();
			foreach (var row in Rows) {
				if (FilterRows(columnIndex, row, predicate)) {
					stringTable.AddRow(row.Values);
				}
			}
			return stringTable;
		}

		internal bool FilterRows(int? columnIndex, Row row, Func<string, bool> predicate) {
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