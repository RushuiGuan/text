using Albatross.Expression;
using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	public class TaskLast : PrefixExpression {
		public TaskLast() : base("takelast", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var instance = operands[0].ConvertToCollection(out var type);
			var count = operands[1].ConvertToInt();
			return instance.TakeLast(count).ToArray();
		}
	}
}