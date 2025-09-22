using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class JsonArray : PrefixExpression {
		public JsonArray() : base("jsonarray", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var input = operands[0].ConvertToCollection(out var type);
			var pointer = JsonPointer.Parse(operands[1].ConvertToString());
			var result = new object[input.Length];
			for(int i=0; i<input.Length; i++) {
				var elem = JsonSerializer.SerializeToElement(input[i], type, FormattedJsonSerialization.Instance.Value);
				var extracted = pointer.Evaluate(elem);
				result[i] = extracted ?? JsonDocument.Parse("null").RootElement;
			}
			return result;
		}
	}
}