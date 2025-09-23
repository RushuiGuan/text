using Albatross.Expression;
using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	public class Last : PrefixExpression {
		public Last() : base("last", 1, 2) {
		}

		protected override object Run(List<object> operands) {
			var instance = operands[0].ConvertToCollection(out var type);
			int count = 1;
			if (operands.Count > 1) {
				count = operands[1].ConvertToInt();
			}
			return instance.TakeLast(count).ToArray();
		}
	}
}