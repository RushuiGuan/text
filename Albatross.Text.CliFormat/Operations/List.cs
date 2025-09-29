using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using System.Reflection;

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
				return result;
			}
		}
	}

	// public static class TestExtensions {
	// 	public static object? GetPropertyValue2(this Type type, object data, string name, bool ignoreCase) {
	// 		var bindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
	// 		if (ignoreCase) {
	// 			bindingFlag = bindingFlag | BindingFlags.IgnoreCase;
	// 		}
	// 		var index = name.IndexOf('.');
	// 		if (index == -1) {
	// 			var property = type.GetProperty(name, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
	// 			return property.GetValue(data);
	// 		} else {
	// 			var firstProperty = name.Substring(0, index);
	// 			var property = type.GetProperty(firstProperty, bindingFlag) ?? throw new ArgumentException($"Property {name} is not found in type {type.Name}");
	// 			var value = property.GetValue(data);
	// 			if (value != null) {
	// 				var remainingProperty = name.Substring(index + 1);
	// 				// use value.GetType() instead of property.PropertyType because property.PropertyType may be a base class of value
	// 				return GetPropertyValue2(value.GetType(), value, remainingProperty, ignoreCase);
	// 			} else {
	// 				return null;
	// 			}
	// 		}
	// 	}
	// 	public static object Convert(this object source, string? path) {
	// 		var collection = source.ConvertToCollection(out var elementType);
	// 		if(!string.IsNullOrEmpty(path)) {
	// 			foreach (var item in collection) {
	// 				var value = elementType.GetPropertyValue(item, path, true);
	// 			}
	// 		}
	// 	}
	// }
}

