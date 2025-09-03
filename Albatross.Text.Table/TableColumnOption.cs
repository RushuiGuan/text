using System;

namespace Albatross.Text.Table {
	public class TableColumnOption {
		public TableColumnOption(Func<object, object?> getValueDelegate, Func<object, object?, TextValue> formatter) {
			GetValueDelegate = getValueDelegate;
			Formatter = formatter;
		}

		public Func<object, object?> GetValueDelegate { get; init; }
		public Func<object, object?, TextValue> Formatter { get; init; }
		public required string Header { get; init; }
		public required int Order { get; init; }
		public required string Property { get; init; }
	}

	public class TableColumnOption<T> : TableColumnOption {
		public TableColumnOption(Func<T, object?> getValueDelegate, Func<T, object?, TextValue> formatter)
			: base(args => getValueDelegate((T)args), (args, value) => formatter((T)args, value)) {
		}
	}
}