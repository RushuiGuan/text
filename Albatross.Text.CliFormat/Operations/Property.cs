using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts a property value from an object using dot notation for nested access.
	/// Syntax: property(object, 'PropertyName') or property(object, 'Nested.Property[0]')
	/// </summary>
	public class Property : PrefixExpression {
		/// <summary>
		/// Initializes the Property operation requiring exactly 2 operands.
		/// </summary>
		public Property() : base("property", 2, 2) {
		}

		/// <summary>
		/// Extracts the specified property value from the object.
		/// </summary>
		/// <param name="operands">First operand is the object, second is the property path.</param>
		/// <returns>The property value, or empty string if null.</returns>
		protected override object Run(List<object> operands) {
			var value = operands[0];
			var property = operands[1].ConvertToString();
			var type = value.GetType();
			return type.GetPropertyValue(value, property, false) ?? string.Empty;
		}
	}
}