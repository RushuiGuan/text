using System;

namespace Albatross.Text.Table {
	public class TableColumnOption {
		public TableColumnOption(Func<object, object?> getValueDelegate, Func<object, object?, TextValue> formatter) {
			GetValueDelegate = getValueDelegate;
			Formatter = formatter;
		}

		public Func<object, object?> GetValueDelegate { get; set; }
		public Func<object, object?, TextValue> Formatter { get; set; }
		public required string Header { get; set; }
		public required int Order { get; set; }
		public required string Property { get; set; }
	}

	public class TableColumnOption<T> : TableColumnOption {
		public TableColumnOption(Func<T, object?> getValueDelegate, Func<T, object?, TextValue> formatter)
			: base(args => getValueDelegate((T)args), (args, value) => formatter((T)args, value)) {
		}
	}
}