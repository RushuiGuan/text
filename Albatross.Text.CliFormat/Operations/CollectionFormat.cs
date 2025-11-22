using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;

namespace Albatross.Text.CliFormat.Operations {
	public class CollectionFormat : PrefixExpression {
		public CollectionFormat() : base("cformat", 2, 2) {
		}

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