using System;

namespace Albatross.Text.Table {
	/// <summary>
	/// Configuration for a single table column including its property binding, header text, ordering, and formatting.
	/// </summary>
	public class TableColumnOption {
		public TableColumnOption(string property, Func<object, object?> getValueDelegate, Func<object, object?, TextValue> formatter) {
			GetValueDelegate = getValueDelegate;
			Formatter = formatter;
			Property = property;
		}

		/// <summary>
		/// The source property name this column is bound to.
		/// </summary>
		public string Property { get; }

		/// <summary>
		/// Delegate that extracts the raw value from an entity.
		/// </summary>
		public Func<object, object?> GetValueDelegate { get; set; }

		/// <summary>
		/// Delegate that formats the raw value into a <see cref="TextValue"/> for display.
		/// </summary>
		public Func<object, object?, TextValue> Formatter { get; set; }

		/// <summary>
		/// The display header text for this column.
		/// </summary>
		public required string Header { get; set; }

		/// <summary>
		/// The sort order for column positioning. Lower values appear first.
		/// </summary>
		public required int Order { get; set; }
	}

	/// <summary>
	/// Type-safe column configuration that provides strongly-typed value extraction and formatting.
	/// </summary>
	/// <typeparam name="T">The entity type this column extracts values from.</typeparam>
	public class TableColumnOption<T> : TableColumnOption {
		public TableColumnOption(string property, Func<T, object?> getValueDelegate, Func<T, object?, TextValue> formatter)
			: base(property, args => getValueDelegate((T)args), (args, value) => formatter((T)args, value)) {
		}

		/// <summary>
		/// Replaces the value extraction delegate with a new one.
		/// </summary>
		public TableColumnOption NewGetValueDelegate(Func<T, object?> getValueDelegate) {
			this.GetValueDelegate = args => getValueDelegate((T)args);
			return this;
		}

		/// <summary>
		/// Replaces the formatting delegate with a new one.
		/// </summary>
		public TableColumnOption NewFormatter(Func<T, object?, TextValue> formatter) {
			this.Formatter = (args, value) => formatter((T)args, value);
			return this;
		}
	}
}