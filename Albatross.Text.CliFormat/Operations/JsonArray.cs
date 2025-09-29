using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class JsonArray : PrefixExpression {
		public JsonArray() : base("jsonarray", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var elementType);
			var pointer = global::Json.Pointer.JsonPointer.Parse(operands[1].ConvertToString());
			var result = new List<JsonElement?>();
			foreach(var item in list){
				var elem = JsonSerializer.SerializeToElement(item, elementType, FormattedJsonSerialization.Instance.Value);
				var extracted = pointer.Evaluate(elem);
				result.Add(extracted);
			}
			return JsonSerializer.SerializeToElement(result, FormattedJsonSerialization.Instance.Value);
		}
	}
}