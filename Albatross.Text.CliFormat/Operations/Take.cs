using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Net.WebSockets;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the first n elements from a collection.  The second parameter is optional and defaults to 1.
	/// </summary>
	public class Take : PrefixExpression {
		/// <summary>
		/// Initializes the First operation supporting 1-2 operands.
		/// </summary>
		public Take() : base("take", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var value = operands[0].ConvertToCollection(out _);
			var count = operands[2].ConvertToInt();
			return value.Take(count).ToArray();
		}
	}
}