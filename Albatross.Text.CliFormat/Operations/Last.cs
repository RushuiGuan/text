using Albatross.Expression;
using Albatross.Expression.Prefix;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	public class Last : PrefixExpression {
		public Last() : base("last", 1, 2) {
		}

		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var type);
			int count = 1;
			if (operands.Count > 1) {
				count = operands[1].ConvertToInt();
			}
			if (list.Count <= count) {
				return list;
			} else {
				var array = Array.CreateInstance(type, count);
				for (int i = list.Count - count; i < list.Count; i++) {
					array.SetValue(list[i], i - (list.Count - count));
				}
				return array;
			}
		}
	}
}