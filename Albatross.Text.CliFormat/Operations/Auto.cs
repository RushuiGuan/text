using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Albatross.Text.Table;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	public class Auto : PrefixExpression {
		public Auto() : base("auto", 1, 1) {
		}
		protected override object Run(List<object> operands) {
			var value = operands[0];
			var type = value.GetType();
			if (type.GetCollectionElementType(out var elementType)) {
				return ((IEnumerable)value).StringTable(elementType).PrintConsoleWidth();
			} else {
				return value.PropertyTable().PrintConsoleWidth();
			}
		}
	}
}
