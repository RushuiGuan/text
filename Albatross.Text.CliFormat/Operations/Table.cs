using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Text.Table;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as tabular output with optional column selection.
	/// When no columns are specified, all properties are included. When columns are specified, only those columns are displayed.
	/// </summary>
	public class Table : PrefixExpression {
		/// <summary>
		/// Initializes the Table operation supporting zero or more operands.
		/// </summary>
		public Table() : base("table", 0, int.MaxValue) {
		}

		/// <summary>
		/// Executes table formatting with optional column selection.
		/// </summary>
		/// <param name="operands">The operands list where the first operand is the collection, and remaining operands specify column names to display.</param>
		/// <returns>A formatted table string with rows and columns.</returns>
		/// <remarks>
		/// When no column parameters are provided, all object properties are displayed as columns.
		/// When column parameters are specified, only those properties are included in the specified order.
		/// </remarks>
		protected override object Run(List<object> operands) {
			var items = operands[0].ConvertToCollection(out var elementType);
			var columns = Array.Empty<string>();
			if (operands.Count > 1) {
				columns = operands.Skip(1).Select(x => x.ConvertToString()).ToArray();
			}
			var options = TableOptionFactory.Instance.Get(elementType);
			return new StringTable(items, options).FilterColumns(columns);
		}
	}
}