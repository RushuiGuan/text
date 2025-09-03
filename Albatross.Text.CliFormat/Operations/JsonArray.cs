using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as JSON arrays with each item on a separate line, optionally extracting specific fields using JSON pointers.
	/// </summary>
	public class JsonArray : PrefixExpression {
		/// <summary>
		/// Initializes the JsonArray operation supporting one or more operands.
		/// </summary>
		public JsonArray() : base("jsonarray", 1, int.MaxValue) {
		}

		/// <summary>
		/// Executes JSON array formatting with optional field extraction via JSON pointers.
		/// Each collection item is serialized on a separate line using compact JSON format.
		/// </summary>
		/// <param name="operands">The operands list where the first operand is the collection to format, and remaining operands are JSON pointers for field extraction.</param>
		/// <returns>A multi-line string where each line contains compact JSON for one collection item.</returns>
		/// <remarks>
		/// When JSON pointer operands are provided, only the specified fields are extracted from each item.
		/// The extracted fields are comma-separated on each line. When no pointers are provided, entire objects are serialized.
		/// </remarks>
		protected override object Run(List<object> operands) {
			var writer = new StringWriter();
			var input = operands[0].ConvertToCollection(out var type);
			var pointers = operands.Skip(1).Select(x => JsonPointer.Parse(x.ConvertToString())).ToArray();
			foreach (var item in input) {
				if (pointers.Any()) {
					var elem = JsonSerializer.SerializeToElement(item, type, FormattedJsonSerialization.Instance.Value);
					writer.WriteItems(pointers, ",", (w, t) => {
						var result = t.Evaluate(elem);
						if (result == null) {
							w.Write(string.Empty);
						} else {
							var text = JsonSerializer.Serialize(result, CompactJsonSerialization.Instance.Value);
							w.Write(text);
						}
					});
				} else {
					var text = JsonSerializer.Serialize(item, type, CompactJsonSerialization.Instance.Value);
					writer.Write(text);
				}
				writer.WriteLine();
			}
			return writer.ToString();
		}
	}
}