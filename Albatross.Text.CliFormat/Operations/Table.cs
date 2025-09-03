using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat.Operations {
	public class Table : PrefixExpression {
		public Table() : base("table", 0, int.MaxValue) {
		}

		protected override object Run(List<object> operands) {
			var writer = new StringWriter();
			var items = operands[0].ConvertToCollection(out var type);
			if (operands.Count == 1) {
				items.StringTable(type, null).Print(writer);
			} else {
				var columns = operands.Skip(1).Select(x => x.ConvertToString()).ToArray();
				items.StringTable(type, null).PrintColumns(writer, columns, ",");
			}
			return writer.ToString();
		}
	}
}