using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.Text.Table {
	public class TableOptions {
		public TableOptions(Type type) {
			this.Type = type;
		}

		public Type Type { get; }
		List<TableColumnOption> columnOptions = new List<TableColumnOption>();
		public string[] Headers => columnOptions.Select(x => x.Header).ToArray();
		public bool PrintHeader { get; set; } = true;
		public bool PrintFirstLineSeparator { get; set; } = true;
		public bool PrintLastLineSeparator { get; set; } = true;

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

		public IEnumerable<TableColumnOption> ColumnOptions => this.columnOptions;

		public TableColumnOption GetRequiredColumn(string property)
			=> this.GetColumn(property) ?? throw new ArgumentException($"{property} is not an existing column");

		public TableColumnOption? GetColumn(string property)
			=> this.columnOptions.FirstOrDefault(x => x.Property == property);

		public TableOptions Remove(string property) {
			this.columnOptions.RemoveAll(x => x.Property == property);
			return this;
		}

		public TableOptions SetColumn(string property, Func<object, object?> getValue) {
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
			return this;
		}

		public IEnumerable<TableColumnOption> Build() {
			this.columnOptions = columnOptions.OrderBy(x => x.Order)
				.ThenBy(x => x.Header)
				.ThenBy(x => x.Property).ToList();
			return columnOptions;
		}

		public static string DefaultFormat(object? value) {
			if (value == null) {
				return string.Empty;
			} else {
				// note that an object can never contain an instance of Nullable struct
				switch (value) {
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

	public class TableOptions<T> : TableOptions {
		public TableOptions() : base(typeof(T)) { }
	}
}