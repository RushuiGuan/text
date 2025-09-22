using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as a simple list output, optionally filtering by a specific column.
	/// Each item is displayed on a separate line, with complex objects rendered as property tables.
	/// </summary>
	public class Property : PrefixExpression {
		/// <summary>
		/// Initializes the Property operation supporting 1-2 operands.
		/// </summary>
		public Property() : base("property", 2, 2) {
		}

		/// <summary>
		/// Executes list formatting, displaying each collection item on a separate line.
		/// </summary>
		/// <param name="operands">The operands list: 1) collection, 2) optional column name for filtering.</param>
		/// <returns>A multi-line string with each collection item on a separate line.</returns>
		protected override object Run(Property<object> operands) {
			var value = operands[0].ConvertToCollection(out var type);
			string? column = null;
			if(operands.Count > 1) {
				column = operands[1].ConvertToString();
			}
			return Print(list, type, null, column, false);
		}
		
		/// <summary>
		/// Generates list output from a collection with optional filtering, count limiting, and order reversal.
		/// </summary>
		/// <param name="value">The collection to format as a list.</param>
		/// <param name="type">The element type of the collection.</param>
		/// <param name="count">Optional limit on the number of items to include.</param>
		/// <param name="column">Optional property name to extract from each item instead of the full object.</param>
		/// <param name="reversed">When true and count is specified, takes items from the end of the collection.</param>
		/// <returns>A multi-line string where each line represents one collection item.</returns>
		/// <remarks>
		/// Primitive types and strings are displayed directly. Complex objects are rendered as property tables.
		/// When a column is specified, only that property value is extracted and displayed.
		/// </remarks>
		public static object Print(IEnumerable<object> value, Type type, int? count, string? column, bool reversed) {
			if (count.HasValue) {
				if (reversed) {
					value = value.TakeLast(count.Value);
				} else {
					value = value.TakeLast(count.Value);
				}
			}
			var writer = new StringWriter();
			foreach (var item in value) {
				var target = item;
				if (!string.IsNullOrEmpty(column)) {
					target = type.GetPropertyValue(item, column, true);
				}
				if (target == null) {
					writer.AppendLine(string.Empty);
				} else if (target.GetType().IsPrimitive || target is string) {
					writer.AppendLine(target);
				} else {
					var stringTable = target.PropertyTable(null, null);
					stringTable.Print(writer);
				}
			}
			return writer.ToString();
		}
	}
}