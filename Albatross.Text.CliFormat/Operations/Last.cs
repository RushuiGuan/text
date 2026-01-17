using Albatross.Expression;
using Albatross.Expression.Prefix;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the last element from a collection, returning a single value.
	/// Syntax: last(collection)
	/// </summary>
	public class Last : PrefixExpression {
		/// <summary>
		/// Initializes the Last operation supporting 1 operand.
		/// </summary>
		public Last() : base("last", 1, 1) {
		}

		/// <summary>
		/// Returns the last element of the collection.
		/// </summary>
		/// <param name="operands">Collection to extract from.</param>
		/// <returns>The last element.</returns>
		/// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var type);
			if (list.Count > 0) {
				return list[^1] ?? string.Empty;
			} else {
				throw new InvalidOperationException($"value {list} does not contain any elements");
			}
		}
	}
}