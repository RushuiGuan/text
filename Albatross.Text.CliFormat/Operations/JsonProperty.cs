using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class JsonProperty : PrefixExpression {
		public JsonProperty() : base("jsonproperty", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var pointer = JsonPointer.Parse(operands[1].ConvertToString());
			var doc = JsonSerializer.SerializeToElement(operands[0], FormattedJsonSerialization.Instance.Value);
			return pointer.Evaluate(doc) ?? JsonDocument.Parse("null").RootElement;
		}
	}
}