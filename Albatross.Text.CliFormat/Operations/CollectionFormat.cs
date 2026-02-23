using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Applies a .NET format string to each element in a collection.
	/// Syntax: cformat(collection, 'formatString') - e.g., cformat(prices, 'C') for currency
	/// </summary>
	public class CollectionFormat : PrefixExpression {
		/// <summary>
		/// Initializes the CollectionFormat operation requiring exactly 2 operands.
		/// </summary>
		public CollectionFormat() : base("cformat", 2, 2) {
		}

		/// <summary>
		/// Formats each element in the collection using the specified format string.
		/// </summary>
		/// <param name="operands">First operand is the collection, second is the .NET format string.</param>
		/// <returns>A list of formatted strings.</returns>
		protected override object Run(List<object> operands) {
			var value = operands[0];
			var format = operands[1].ConvertToString();
			if (string.IsNullOrEmpty(format)) {
				return value;
			} else {
				var list = value.ConvertToCollection(out _);
				var results = new List<string>();
				foreach (var item in list) {
					results.Add(string.Format(System.Globalization.CultureInfo.InvariantCulture, $"{{0:{format}}}", item));
				}
				return results;
			}
		}
	}
}