using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text.Table {
	public static class StringTableExtensions {
		public static StringTable StringTable<T>(this IEnumerable<T> items, TableOptions<T>? options = null) {
			options = options ?? TableOptionFactory.Instance.Get<T>();
			StringTable table = new StringTable(options.ColumnOptions.Select(x => x.Header));
			foreach (var item in items) {
				table.Add(options.GetValue(item));
			}
			return table;
		}

		public static StringTable PropertyTable<T>(this T instance, StringTable? table = null) {
			var dictionary = new Dictionary<string, object>();
			Albatross.Reflection.Enumerations.Property(instance, null, null, dictionary);
			const string PropertyColumn = "Property";
			const string ValueColumn = "Value";
			if (table == null) {
				table = new StringTable(PropertyColumn, ValueColumn);
			}
			foreach (var item in dictionary) {
				table.Add(new TextValue(item.Key), new TextValue(TextOptionBuilderExtensions.DefaultFormat(item.Value)));
			}
			return table;
		}

		public static StringTable SetColumn(this StringTable table, Func<StringTable.Column, bool> predicate, Action<StringTable.Column> action) {
			foreach (var column in table.Columns.Where(x => predicate(x))) {
				action(column);
			}
			return table;
		}

		public static StringTable MinWidth(this StringTable table, Func<StringTable.Column, bool> predicate, int minWidth)
			=> table.SetColumn(predicate, x => x.MinWidth = minWidth);


		public static StringTable AlignRight(this StringTable table, Func<StringTable.Column, bool> predicate, bool value = true)
			=> table.SetColumn(predicate, x => x.AlignRight = value);

		public static StringTable SetWidthLimit(this StringTable table, int width) {
			table.AdjustColumnWidth(width);
			return table;
		}

		public static void PrintConsole(this StringTable table) {
			table.AdjustColumnWidth(GetConsoleWith());
			table.Print(System.Console.Out);
		}

		public static int GetConsoleWith() {
			if (System.Console.IsOutputRedirected) {
				return int.MaxValue;
			} else {
				return System.Console.BufferWidth;
			}
		}

		public static string Print(this StringTable table) {
			var writer = new StringWriter();
			table.Print(writer);
			return writer.ToString();
		}
	}
}
