using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Albatross.Text.Table {
	/// <summary>
	/// Fluent API extension methods for configuring <see cref="TableOptions{T}"/>.
	/// </summary>
	public static class TableOptionsExtension {
		/// <summary>
		/// Sets the display order for a column. Lower values appear first.
		/// </summary>
		public static TableOptions<T> ColumnOrder<T>(this TableOptions<T> options, string property, int order) {
			var column = options.GetRequiredColumn(property);
			column.Order = order;
			return options;
		}

		/// <summary>
		/// Removes a column from the table output.
		/// </summary>
		public static TableOptions<T> Ignore<T>(this TableOptions<T> options, string property) {
			options.Remove(property);
			return options;
		}

		/// <summary>
		/// Changes the header text for a column.
		/// </summary>
		public static TableOptions<T> ColumnHeader<T>(this TableOptions<T> options, string property, string newHeader) {
			var column = options.GetRequiredColumn(property);
			column.Header = newHeader;
			return options;
		}

		/// <summary>
		/// Sets a custom formatter for a column's values.
		/// </summary>
		public static TableOptions<T> Format<T>(this TableOptions<T> options, string property, Func<T, object?, TextValue> format) {
			var column = options.GetRequiredColumn(property);
			column.Formatter = (entity, value)=>format((T)entity, value);
			return options;
		}

		/// <summary>
		/// Controls whether column headers are printed.
		/// </summary>
		public static TableOptions<T> PrintHeader<T>(this TableOptions<T> options, bool print) {
			options.PrintHeader = print;
			return options;
		}

		/// <summary>
		/// Controls whether a separator line is printed after the header.
		/// </summary>
		public static TableOptions<T> PrintFirstLineSeparator<T>(this TableOptions<T> options, bool print) {
			options.PrintFirstLineSeparator = print;
			return options;
		}

		/// <summary>
		/// Controls whether a separator line is printed after the last row.
		/// </summary>
		public static TableOptions<T> PrintLastLineSeparator<T>(this TableOptions<T> options, bool print) {
			options.PrintLastLineSeparator = print;
			return options;
		}

		/// <summary>
		/// Adds or updates a column with a custom value getter.
		/// </summary>
		public static TableOptions<T> SetColumn<T>(this TableOptions<T> options,  string property, Func<T, object?> getValue) {
			options.SetColumn(property, (entity) => getValue((T)entity));
			return options;
		}
	}
}