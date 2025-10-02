using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as a simple list output, optionally filtering by a specific column.
	/// Each item is displayed on a separate line, with complex objects rendered as property tables.
	/// </summary>
	public class List : PrefixExpression {
		/// <summary>
		/// Initializes the List operation supporting 1-2 operands.
		/// </summary>
		public List() : base("list", 1, 2) {
		}

		/// <summary>
		/// Executes list formatting, displaying each collection item on a separate line.
		/// </summary>
		/// <param name="operands">The operands list: 1) collection, 2) optional column name for filtering.</param>
		/// <returns>A multi-line string with each collection item on a separate line.</returns>
		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var elementType);
			if (elementType == typeof(string) || elementType.IsPrimitive) {
				return list;
			} else {
				var result = new List<Dictionary<string, object>>();
				var path = operands.Count > 1 ? operands[1].ConvertToString() : null;
				foreach (var item in list) {
					var dictionary = new Dictionary<string, object>();
					var instance = string.IsNullOrEmpty(path) ? item : elementType.GetPropertyValue(item, path, true);
					instance.ToDictionary(dictionary);
					result.Add(dictionary);
				}
				return result.Cast<IDictionary>().ToArray();
			}
		}
	}
}

