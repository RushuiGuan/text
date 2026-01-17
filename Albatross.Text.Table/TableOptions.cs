using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.Text.Table {
	/// <summary>
	/// Configuration for rendering collections as formatted text tables. Defines columns, headers, and formatting rules.
	/// </summary>
	public class TableOptions {
		public TableOptions(Type type) {
			this.Type = type;
		}

		/// <summary>
		/// The element type this configuration is designed for.
		/// </summary>
		public Type Type { get; }
		List<TableColumnOption> columnOptions = new List<TableColumnOption>();

		/// <summary>
		/// Array of column header names in display order.
		/// </summary>
		public string[] Headers => columnOptions.Select(x => x.Header).ToArray();

		/// <summary>
		/// Whether to print column headers. Default is true.
		/// </summary>
		public bool PrintHeader { get; set; } = true;

		/// <summary>
		/// Whether to print a separator line after the header. Default is true.
		/// </summary>
		public bool PrintFirstLineSeparator { get; set; } = true;

		/// <summary>
		/// Whether to print a separator line after the last row. Default is true.
		/// </summary>
		public bool PrintLastLineSeparator { get; set; } = true;

		/// <summary>
		/// Extracts formatted values from an item for each configured column.
		/// </summary>
		public TextValue[] GetValue(object? item) {
			var array = new TextValue[columnOptions.Count];
			for (int i = 0; i < columnOptions.Count; i++) {
				if (item == null) {
					array[i] = TextValue.Empty;
				} else {
					var value = columnOptions[i].GetValueDelegate(item);
					array[i] = columnOptions[i].Formatter(item, value);
				}
			}
			return array;
		}

		/// <summary>
		/// The collection of column configurations.
		/// </summary>
		public IEnumerable<TableColumnOption> ColumnOptions => this.columnOptions;

		/// <summary>
		/// Gets a column by property name, throwing if not found.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when the property is not an existing column.</exception>
		public TableColumnOption GetRequiredColumn(string property)
			=> this.GetColumn(property) ?? throw new ArgumentException($"{property} is not an existing column");

		/// <summary>
		/// Gets a column by property name, or null if not found.
		/// </summary>
		public TableColumnOption? GetColumn(string property)
			=> this.columnOptions.FirstOrDefault(x => x.Property == property);

		/// <summary>
		/// Removes a column from the configuration by property name.
		/// </summary>
		public TableOptions Remove(string property) {
			this.columnOptions.RemoveAll(x => x.Property == property);
			return this;
		}

		/// <summary>
		/// Adds or updates a column configuration with the specified value getter.
		/// </summary>
		public void SetColumn(string property, Func<object, object?> getValue) {
			var column = GetColumn(property);
			if (column == null) {
				column = new TableColumnOption(property, getValue, (_, value) => new TextValue(DefaultFormat(value))) {
					Header = property,
					Order = this.columnOptions.Count
				};
				this.columnOptions.Add(column);
			} else {
				column.GetValueDelegate = getValue;
			}
		}

		/// <summary>
		/// Finalizes column ordering and returns the sorted column configurations.
		/// </summary>
		public IEnumerable<TableColumnOption> Build() {
			this.columnOptions = columnOptions.OrderBy(x => x.Order)
				.ThenBy(x => x.Header)
				.ThenBy(x => x.Property).ToList();
			return columnOptions;
		}

		/// <summary>
		/// Provides default string formatting for common types including dates, decimals, and primitives.
		/// </summary>
		public static string DefaultFormat(object? value) {
			if (value == null) {
				return string.Empty;
			} else {
				// note that an object can never contain an instance of Nullable struct
				switch (value) {
					case string text:
						return text;
					case DateOnly date:
						return $"{date:yyyy-MM-dd}";
					case TimeOnly time:
						return $"{time:HH:mm:ss}";
					case DateTime dateTime:
						return $"{dateTime:yyyy-MM-ddTHH:mm:ssK}";
					case DateTimeOffset dateTimeOffset:
						return $"{dateTimeOffset:yyyy-MM-ddTHH:mm:ssK}";
					case decimal d:
						return d.Decimal2CompactText();
					default:
						return Convert.ToString(value) ?? string.Empty;
				}
			}
		}
	}

	/// <summary>
	/// Generic table configuration providing type-safe column and formatting configuration.
	/// </summary>
	/// <typeparam name="T">The element type of collections to be rendered.</typeparam>
	public class TableOptions<T> : TableOptions {
		public TableOptions() : base(typeof(T)) { }
	}
}