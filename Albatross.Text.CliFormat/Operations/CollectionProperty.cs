using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	public class CollectionProperty : PrefixExpression {
		public CollectionProperty() : base("cproperty", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var elementType);
			var property = operands[1].ConvertToString();

			Array array = Array.CreateInstance(elementType.GetPropertyType(property, false), list.Count);
			var index = 0;
			foreach (var item in list) {
				var value = elementType.GetPropertyValue(item, property, false);
				array.SetValue(value, index);
				index++;
			}
			return array;
		}
	}
}