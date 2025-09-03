using System;
using System.Collections.Generic;
using System.Linq;

namespace Albatross.Text.Table {
	public class TableOptions {
		public TableOptions(Type type) {
			this.Type = type;
		}

		public Type Type { get; }
		public TableColumnOption[] ColumnOptions { get; init; } = [];
		public string[] Headers => ColumnOptions.Select(x => x.Header).ToArray();

		public TextValue[] GetValue(object item) {
			var array = new TextValue[ColumnOptions.Length];
			for (int i = 0; i < ColumnOptions.Length; i++) {
				var value = ColumnOptions[i].GetValueDelegate(item);
				array[i] = ColumnOptions[i].Formatter(item, value);
			}
			return array;
		}
	}

	public class TableOptions<T> : TableOptions {
		public TableOptions(TableOptionBuilder<T> builder) : base(typeof(T)) {
			var list = new List<TableColumnOption>();
			foreach (var keyValue in builder.ColumnOptionBuilders) {
				list.Add(new TableColumnOption<T>(keyValue.Value.GetValueDelegate, keyValue.Value.Formatter) {
					Header = keyValue.Value.GetHeader(),
					Order = keyValue.Value.GetOrder(),
					Property = keyValue.Key,
				});
			}
			this.ColumnOptions = list.OrderBy(x => x.Order)
				.ThenBy(x => x.Header)
				.ThenBy(x => x.Property)
				.ToArray();
		}
	}
}