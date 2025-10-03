using Albatross.Expression;
using Albatross.Expression.Prefix;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	public class Last : PrefixExpression {
		public Last() : base("last", 1, 1) {
		}

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