using Albatross.Expression.Prefix;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as a simple list output, optionally filtering by a specific column.
	/// Each item is displayed on a separate line, with complex objects rendered as property tables.
	/// </summary>
	public class List : PrefixExpression {
		/// <summary>
		/// Initializes the List operation supporting 1-2 operands.
		/// </summary>
		public List() : base("list", 1, 1) {
		}

		/// <summary>
		/// Executes list formatting, displaying each collection item on a separate line.
		/// </summary>
		/// <param name="operands">The operands list: 1) collection, 2) optional column name for filtering.</param>
		/// <returns>A multi-line string with each collection item on a separate line.</returns>
		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var type);
			return Print(list, type);
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
		public static object Print(IEnumerable<object> value, Type type) {
			var writer = new StringWriter();
			foreach (var item in value) {
				if (item.GetType().IsPrimitive || item is string) {
					writer.AppendLine(item);
				} else {
					var stringTable = item.PropertyTable(null, null);
					stringTable.Print(writer);
				}
			}
			return writer.ToString();
		}
	}
}