using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats objects as indented JSON output, optionally extracting specific data using JSON pointers.
	/// </summary>
	public class Json : PrefixExpression {
		/// <summary>
		/// Initializes the Json operation supporting 1-2 operands.
		/// </summary>
		public Json() : base("json", 1, 2) {
		}

		/// <summary>
		/// Executes JSON formatting with optional JSON pointer extraction.
		/// </summary>
		/// <param name="operands">The operands list containing the object to serialize and optionally a JSON pointer for data extraction.</param>
		/// <returns>A formatted JSON string. If a JSON pointer is provided, returns only the extracted portion.</returns>
		protected override object Run(List<object> operands) {
			if (operands.Count == 1) {
				return JsonSerializer.Serialize(operands[0], FormattedJsonSerialization.Instance.Value);
			} else {
				var pointer = JsonPointer.Parse(operands[1].ConvertToString());
				var doc = JsonSerializer.SerializeToElement(operands[0], FormattedJsonSerialization.Instance.Value);
				var result = pointer.Evaluate(doc);
				return JsonSerializer.Serialize(result, FormattedJsonSerialization.Instance.Value);
			}
		}
	}
}