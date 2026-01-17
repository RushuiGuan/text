using Albatross.Expression;
using Albatross.Expression.Prefix;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Applies a JSON Pointer to each element in a collection, returning an array of extracted values.
	/// Syntax: cjsonpointer(collection, /path/to/property)
	/// </summary>
	public class CollectionJsonPointer : PrefixExpression {
		/// <summary>
		/// Initializes the CollectionJsonPointer operation requiring exactly 2 operands.
		/// </summary>
		public CollectionJsonPointer() : base("cjsonpointer", 2, 2) {
		}

		/// <summary>
		/// Evaluates the JSON pointer against each element in the collection.
		/// </summary>
		/// <param name="operands">First operand is the collection, second is the JSON pointer path.</param>
		/// <returns>A JSON array containing the extracted values from each element.</returns>
		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var elementType);
			var pointer = global::Json.Pointer.JsonPointer.Parse(operands[1].ConvertToString());
			var array = new System.Text.Json.Nodes.JsonArray();
			foreach (var item in list) {
				var elem = JsonSerializer.SerializeToElement(item, elementType);
				var extracted = pointer.Evaluate(elem);
				array.Add(extracted);
			}
			return JsonSerializer.SerializeToElement(array);
		}
	}
}