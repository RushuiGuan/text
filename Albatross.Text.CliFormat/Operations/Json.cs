using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class Json : PrefixExpression {
		public Json() : base("json", 1, 2) {
		}

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