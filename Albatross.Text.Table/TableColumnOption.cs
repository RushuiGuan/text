using System;

namespace Albatross.Text.Table {
	public class TableColumnOption {
		public TableColumnOption(string property, Func<object, object?> getValueDelegate, Func<object, object?, TextValue> formatter) {
			GetValueDelegate = getValueDelegate;
			Formatter = formatter;
			Property = property;
		}
		public string Property { get; }
		public Func<object, object?> GetValueDelegate { get; set; }
		public Func<object, object?, TextValue> Formatter { get; set; }
		public required string Header { get; set; }
		public required int Order { get; set; }
	}

	public class TableColumnOption<T> : TableColumnOption {
		public TableColumnOption(string property, Func<T, object?> getValueDelegate, Func<T, object?, TextValue> formatter)
			: base(property, args => getValueDelegate((T)args), (args, value) => formatter((T)args, value)) {
		}

		public TableColumnOption NewGetValueDelegate(Func<T, object?> getValueDelegate) {
			this.GetValueDelegate = args => getValueDelegate((T)args);
			return this;
		}
		public TableColumnOption NewFormatter(Func<T, object?, TextValue> formatter) {
			this.Formatter = (args, value) => formatter((T)args, value);
			return this;
		}
	}
}