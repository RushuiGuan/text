using Albatross.Expression;
using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the last elements from a collection, optionally filtering by a specific column and limiting the count.
	/// </summary>
	public class Last : PrefixExpression {
		/// <summary>
		/// Initializes the Last operation supporting 1-3 operands.
		/// </summary>
		public Last() : base("last", 1, 3) {
		}

		/// <summary>
		/// Executes the last elements extraction from the end of a collection.
		/// </summary>
		/// <param name="operands">The operands list: 1) collection, 2) optional column name, 3) optional count (defaults to 1).</param>
		/// <returns>A formatted string containing the last elements from the collection.</returns>
		/// <remarks>
		/// When a column name is specified, only that property value is displayed for each item.
		/// The count parameter determines how many elements to take from the end of the collection.
		/// </remarks>
		protected override object Run(List<object> operands) {
			var instance = operands[0].ConvertToCollection(out var type);
			var count = 1;
			string? column = null;
			if (operands.Count > 1) {
				column = operands[1].ConvertToString();
				if (operands.Count > 2) {
					count = operands[2].ConvertToInt();
				}
			}
			return List.Print(instance, type, count, column, true);
		}
	}
}