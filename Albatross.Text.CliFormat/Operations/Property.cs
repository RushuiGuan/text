using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;

namespace Albatross.Text.CliFormat.Operations {
	public class Property : PrefixExpression {
		public Property() : base("property", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var value = operands[0];
			var property = operands[1].ConvertToString();
			var type = value.GetType();
			return type.GetPropertyValue(value, property, true) ?? string.Empty;
		}
	}
}