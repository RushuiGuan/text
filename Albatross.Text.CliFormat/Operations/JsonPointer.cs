using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Extracts data from objects using JSON Pointer syntax (RFC 6901).
	/// Syntax: jsonpointer(value, /path/to/property)
	/// </summary>
	public class JsonPointer : PrefixExpression {
		/// <summary>
		/// Initializes the JsonPointer operation requiring exactly 2 operands.
		/// </summary>
		public JsonPointer() : base("jsonpointer", 2, 2) {
		}

		/// <summary>
		/// Evaluates the JSON pointer against the serialized input object.
		/// </summary>
		/// <param name="operands">First operand is the object, second is the JSON pointer path.</param>
		/// <returns>The extracted JSON element, or null if the path doesn't exist.</returns>
		protected override object Run(List<object> operands) {
			var pointer = global::Json.Pointer.JsonPointer.Parse(operands[1].ConvertToString());
			var doc = JsonSerializer.SerializeToElement(operands[0]);
			return pointer.Evaluate(doc) ?? JsonDocument.Parse("null").RootElement;
		}
	}
}