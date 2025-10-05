using Albatross.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text.Table {
	public static class StringTableExtensions {
		public static StringTable StringTable<T>(this IEnumerable<T> items, TableOptions? options = null) {
			options = options ?? TableOptionFactory.Instance.Get<T>();
			return new StringTable(items, options);
		}

		public static StringTable StringTable(this IDictionary dictionary) {
			StringTable table = new StringTable("Key", "Value");
			foreach (DictionaryEntry entry in dictionary) {
				table.AddRow(new TextValue(TableOptions.DefaultFormat(entry.Key)), new TextValue(TableOptions.DefaultFormat(entry.Value)));
			}
			return table;
		}

		public static StringTable ChangeColumn(this StringTable table, Func<StringTable.Column, bool> predicate, Action<StringTable.Column> action) {
			foreach (var column in table.Columns.Where(predicate)) {
				action(column);
			}
			return table;
		}

		public static StringTable MinWidth(this StringTable table, Func<StringTable.Column, bool> predicate, int minWidth)
			=> table.ChangeColumn(predicate, x => x.MinWidth = minWidth);

		public static StringTable AlignRight(this StringTable table, Func<StringTable.Column, bool> predicate, bool value = true)
			=> table.ChangeColumn(predicate, x => x.AlignRight = value);

		public static StringTable SetWidthLimit(this StringTable table, int width) {
			table.AdjustColumnWidth(width);
			return table;
		}

		public static void PrintConsole(this StringTable table, TextWriter writer, int maxWidth = int.MaxValue) {
			table.AdjustColumnWidth(maxWidth);
			table.Print(writer);
		}

		public static int GetConsoleWith() {
			if (System.Console.IsOutputRedirected) {
				return int.MaxValue;
			} else {
				return System.Console.BufferWidth;
			}
		}

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