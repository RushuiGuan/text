using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat.Operations {
	public class Property : PrefixExpression {
		public Property() : base("Property", 1, 2) {
		}

		protected override object Run(List<object> operands) {
			string? path = null;
			if (operands.Count > 1) {
				path  = operands[2].ConvertToString();
			}
			var stringTable = operands[0].PropertyTable(path, null);
			var writer = new StringWriter();
			stringTable.Print(writer);
			return writer.ToString();
		}
	}
}