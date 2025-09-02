using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Text.Table;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	public class Table : PrefixExpression {
		public Table() : base("table", 0, int.MaxValue) {
		}


		IEnumerable GetCollection(object input) {
			if (input is string) {
				return new[] { input };
			} else if (input is IEnumerable enumerable) {
				return enumerable;
			} else {
				return new[] { input };
			}
		}

		protected override object Run(List<object> operands) {
			var writer = new StringWriter();
			var items = GetCollection(operands[0]);
			if (operands.Count == 1) {
				items.StringTable().Print(writer);
			} else {
				var columns = operands.Skip(1).Select(x => x.ConvertToString()).ToArray();
				items.StringTable().PrintColumns(writer, columns, ",");
			}
			return writer.ToString();
		}
	}
}