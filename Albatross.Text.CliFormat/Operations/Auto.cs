using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Albatross.Text.Table;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Automatically selects the appropriate output format based on the input type. 
	/// Uses table format for collections and property table format for single objects.
	/// </summary>
	public class Auto : PrefixExpression {
		/// <summary>
		/// Initializes the Auto operation with exactly one operand.
		/// </summary>
		public Auto() : base("auto", 1, 1) {
		}
		/// <summary>
		/// Executes the auto-formatting operation, determining the best output format for the input.
		/// Collections are rendered as tables while single objects are rendered as property-value pairs.
		/// </summary>
		/// <param name="operands">The operands list containing the single value to format.</param>
		/// <returns>A formatted string representation appropriate for the input type.</returns>
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
