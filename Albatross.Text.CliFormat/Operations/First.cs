using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Net.WebSockets;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the first element from a collection, returning a single value.
	/// Syntax: first(collection)
	/// </summary>
	public class First : PrefixExpression {
		/// <summary>
		/// Initializes the First operation supporting 1 operand.
		/// </summary>
		public First() : base("first", 1, 1) {
		}

		/// <summary>
		/// Returns the first element of the collection.
		/// </summary>
		/// <param name="operands">Collection to extract from.</param>
		/// <returns>The first element.</returns>
		/// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
		protected override object Run(List<object> operands) {
			var value = operands[0].ConvertToCollection(out var elementType);
			if (value.Count > 0) {
				return value[0] ?? string.Empty;
			} else {
				throw new InvalidOperationException($"value {value} does not contain any elements");
			}
		}
	}
}