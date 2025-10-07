using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Albatross.Text.Table {
	public static class TableOptionsExtension {
		public static TableOptions<T> ColumnOrder<T>(this TableOptions<T> options, string property, int order) {
			var column = options.GetRequiredColumn(property);
			column.Order = order;
			return options;
		}

		public static TableOptions<T> Ignore<T>(this TableOptions<T> options, string property) {
			options.Remove(property);
			return options;
		}

	
		public static TableOptions<T> ColumnHeader<T>(this TableOptions<T> options, string property, string newHeader) {
			var column = options.GetRequiredColumn(property);
			column.Header = newHeader;
			return options;
		}

		public static TableOptions<T> Format<T>(this TableOptions<T> options, string property, Func<T, object?, TextValue> format) {
			var column = options.GetRequiredColumn(property);
			column.Formatter = (entity, value)=>format((T)entity, value);
			return options;
		}

		public static TableOptions<T> PrintHeader<T>(this TableOptions<T> options, bool print) {
			options.PrintHeader = print;
			return options;
		}

		public static TableOptions<T> PrintFirstLineSeparator<T>(this TableOptions<T> options, bool print) {
			options.PrintFirstLineSeparator = print;
			return options;
		}

		public static TableOptions<T> PrintLastLineSeparator<T>(this TableOptions<T> options, bool print) {
			options.PrintLastLineSeparator = print;
			return options;
		}

		public static TableOptions<T> SetColumn<T>(this TableOptions<T> options,  string property, Func<T, object?> getValue) {
			options.SetColumn(property, (entity) => getValue((T)entity));
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