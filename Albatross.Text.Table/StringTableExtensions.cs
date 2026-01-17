using Albatross.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text.Table {
	/// <summary>
	/// Extension methods for creating and manipulating <see cref="StringTable"/> instances.
	/// </summary>
	public static class StringTableExtensions {
		/// <summary>
		/// Creates a StringTable from a typed collection using the factory-registered or default configuration.
		/// </summary>
		public static StringTable StringTable<T>(this IEnumerable<T> items, TableOptions? options = null) {
			options = options ?? TableOptionFactory.Instance.Get<T>();
			return new StringTable(items, options);
		}

		/// <summary>
		/// Creates a two-column Key/Value StringTable from a dictionary.
		/// </summary>
		public static StringTable StringTable(this IDictionary dictionary) {
			StringTable table = new StringTable("Key", "Value");
			foreach (DictionaryEntry entry in dictionary) {
				table.AddRow(new TextValue(TableOptions.DefaultFormat(entry.Key)), new TextValue(TableOptions.DefaultFormat(entry.Value)));
			}
			return table;
		}

		/// <summary>
		/// Applies an action to columns matching the predicate.
		/// </summary>
		public static StringTable ChangeColumn(this StringTable table, Func<StringTable.Column, bool> predicate, Action<StringTable.Column> action) {
			foreach (var column in table.Columns.Where(predicate)) {
				action(column);
			}
			return table;
		}

		/// <summary>
		/// Sets the minimum width for columns matching the predicate.
		/// </summary>
		public static StringTable MinWidth(this StringTable table, Func<StringTable.Column, bool> predicate, int minWidth)
			=> table.ChangeColumn(predicate, x => x.MinWidth = minWidth);

		/// <summary>
		/// Enables right alignment for columns matching the predicate.
		/// </summary>
		public static StringTable AlignRight(this StringTable table, Func<StringTable.Column, bool> predicate, bool value = true)
			=> table.ChangeColumn(predicate, x => x.AlignRight = value);

		/// <summary>
		/// Adjusts column widths to fit within the specified total width.
		/// </summary>
		public static StringTable SetWidthLimit(this StringTable table, int width) {
			table.AdjustColumnWidth(width);
			return table;
		}

		/// <summary>
		/// Prints the table to the writer, adjusting widths to fit within maxWidth.
		/// </summary>
		public static void PrintConsole(this StringTable table, TextWriter writer, int maxWidth = int.MaxValue) {
			table.AdjustColumnWidth(maxWidth);
			table.Print(writer);
		}

		/// <summary>
		/// Returns the console buffer width, or int.MaxValue if output is redirected.
		/// </summary>
		public static int GetConsoleWith() {
			if (System.Console.IsOutputRedirected) {
				return int.MaxValue;
			} else {
				return System.Console.BufferWidth;
			}
		}

		/// <summary>
		/// Aligns multiple tables to match the first table's column widths and alignment.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when tables have different column counts.</exception>
		public static void AlignFirst(this IEnumerable<StringTable> tables) {
			int? columnCount = null;
			StringTable? first = null;
			foreach (var table in tables) {
				if (columnCount == null) {
					columnCount = table.Columns.Length;
				} else if (columnCount != table.Columns.Length) {
					throw new ArgumentException("Cannot align tables with different number of columns");
				}
				if (first == null) {
					first = table;
				}
				for (int i = 1; i < table.Columns.Length; i++) {
					var firstColumn = first.Columns[i];
					var column = table.Columns[i];
					column.SetMaxTextWidth(firstColumn.DisplayWidth);
					column.AlignRight = firstColumn.AlignRight;
				}
			}
		}

		/// <summary>
		/// Aligns multiple tables using the maximum width across all tables for each column.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when tables have different column counts.</exception>
		public static void AlignAll(this IEnumerable<StringTable> tables) {
			int? columnCount = null;
			Dictionary<int, int> maxWidths = new Dictionary<int, int>();
			Dictionary<int, bool> alignRights = new Dictionary<int, bool>();
			foreach (var table in tables) {
				if (columnCount == null) {
					columnCount = table.Columns.Length;
				} else if (columnCount != table.Columns.Length) {
					throw new ArgumentException("Cannot align tables with different number of columns");
				}
				for (int i = 0; i < table.Columns.Length; i++) {
					if (!maxWidths.TryGetValue(i, out var maxWidth) || table.Columns[i].DisplayWidth > maxWidth) {
						maxWidths[i] = table.Columns[i].DisplayWidth;
					}
					if (!alignRights.TryGetValue(i, out var alignRight) || table.Columns[i].AlignRight) {
						alignRights[i] = table.Columns[i].AlignRight;
					}
				}
			}
			foreach (var table in tables) {
				for (int i = 0; i < table.Columns.Length; i++) {
					var column = table.Columns[i];
					column.SetMaxTextWidth(maxWidths[i]);
				}
			}
		}
	}
}