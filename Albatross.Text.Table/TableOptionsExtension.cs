using System;
using System.Linq;

namespace Albatross.Text.Table {
	public static class TableOptionsExtension {
		public static TableOptions ColumnOrder(this TableOptions options, string property, int order) {
			var column = options.GetRequiredColumn(property);
			column.Order = order;
			return options;
		}

		public static TableOptions Ignore(this TableOptions options, string property) {
			options.Remove(property);
			return options;
		}

	
		public static TableOptions ColumnHeader(this TableOptions options, string property, string newHeader) {
			var column = options.GetRequiredColumn(property);
			column.Header = newHeader;
			return options;
		}

		public static TableOptions Format(this TableOptions options, string property, Func<object, object?, TextValue> format) {
			var column = options.GetRequiredColumn(property);
			column.Formatter = format;
			return options;
		}

		public static TableOptions PrintHeader(this TableOptions options, bool print) {
			options.PrintHeader = print;
			return options;
		}

		public static TableOptions PrintFirstLineSeparator(this TableOptions options, bool print) {
			options.PrintFirstLineSeparator = print;
			return options;
		}

		public static TableOptions PrintLastLineSeparator(this TableOptions options, bool print) {
			options.PrintLastLineSeparator = print;
			return options;
		}

		public static TableOptions<T> Cast<T>(this TableOptions options) {
			if (options.Type != typeof(T)) {
				throw new InvalidCastException($"Cannot cast TableOptions of type {options.Type} to TableOptions<{typeof(T)}>");
			}
			return (TableOptions<T>)options;
		}
	}
}