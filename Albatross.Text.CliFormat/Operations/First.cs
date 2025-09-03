using Albatross.Expression;
using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the first elements from a collection, optionally filtering by a specific column and limiting the count.
	/// </summary>
	public class First : PrefixExpression {
		/// <summary>
		/// Initializes the First operation supporting 1-3 operands.
		/// </summary>
		public First() : base("first", 1, 3) {
		}

		/// <summary>
		/// Executes the first elements extraction from the beginning of a collection.
		/// </summary>
		/// <param name="operands">The operands list: 1) collection, 2) optional column name, 3) optional count (defaults to 1).</param>
		/// <returns>A formatted string containing the first elements from the collection.</returns>
		/// <remarks>
		/// When a column name is specified, only that property value is displayed for each item.
		/// The count parameter determines how many elements to take from the beginning of the collection.
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
			return List.Print(instance, type, count, column, false);
		}
	}
}