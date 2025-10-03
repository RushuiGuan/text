using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Net.WebSockets;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the first element from a collection.  The opeation will return a single value
	/// </summary>
	public class First : PrefixExpression {
		/// <summary>
		/// Initializes the First operation supporting 1 operands.
		/// </summary>
		public First() : base("first", 1, 1) {
		}

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