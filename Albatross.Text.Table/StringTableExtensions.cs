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

		[Obsolete]
		public static StringTable PropertyTable(this object instance, string? path = null, StringTable? table = null) {
			object? target;
			if (!string.IsNullOrEmpty(path)) {
				target = instance.GetType().GetPropertyValue(instance, path, true);
			} else {
				target = instance;
			}
			var dictionary = new Dictionary<string, object>();
			target.ToDictionary(dictionary);
			const string propertyColumn = "Property";
			const string valueColumn = "Value";
			if (table == null) {
				table = new StringTable(propertyColumn, valueColumn);
			}
			foreach (var item in dictionary) {
				table.AddRow(new TextValue(item.Key), new TextValue(TableOptions.DefaultFormat(item.Value)));
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

		public static void Align(this StringTable srcTable, StringTable other, bool useSourceWidth) {
			if (srcTable.Columns.Length != other.Columns.Length) {
				throw new ArgumentException("Cannot align tables with different number of columns");
			}
			for (int i = 0; i < srcTable.Columns.Length; i++) {
				var srcColumn = srcTable.Columns[i];
				var otherColumn = other.Columns[i];
				if (useSourceWidth || otherColumn.DisplayWidth < srcColumn.DisplayWidth) {
					otherColumn.DisplayWidth = srcColumn.DisplayWidth;
				} else {
					srcColumn.DisplayWidth = otherColumn.DisplayWidth;
				}
				otherColumn.AlignRight = srcColumn.AlignRight;
			}
		}
	}
}