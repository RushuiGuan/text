using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts a property value from each element in a collection, returning an array of values.
	/// Syntax: cproperty(collection, 'PropertyName')
	/// </summary>
	public class CollectionProperty : PrefixExpression {
		/// <summary>
		/// Initializes the CollectionProperty operation requiring exactly 2 operands.
		/// </summary>
		public CollectionProperty() : base("cproperty", 2, 2) {
		}

		/// <summary>
		/// Extracts the specified property from each element in the collection.
		/// </summary>
		/// <param name="operands">First operand is the collection, second is the property name.</param>
		/// <returns>An array of property values from each element.</returns>
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