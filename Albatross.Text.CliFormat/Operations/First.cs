using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Net.WebSockets;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts the first n elements from a collection.  The second parameter is optional and defaults to 1.
	/// </summary>
	public class First : PrefixExpression {
		/// <summary>
		/// Initializes the First operation supporting 1-2 operands.
		/// </summary>
		public First() : base("first", 1, 2) {
		}

		protected override object Run(List<object> operands) {
			var value = operands[0].ConvertToCollection(out _);
			int count = 1;
			if (operands.Count > 1) {
				count = operands[1].ConvertToInt();
			}
			return value.Take(count).ToArray();
		}
	}
}