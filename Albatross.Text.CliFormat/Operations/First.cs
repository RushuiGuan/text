using Albatross.Expression;
using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	public class First : PrefixExpression {
		public First() : base("first", 1, 3) {
		}

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