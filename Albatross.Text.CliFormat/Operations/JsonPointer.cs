using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class JsonPointer : PrefixExpression {
		public JsonPointer() : base("jsonpointer", 2, 2) {
		}

		protected override object Run(List<object> operands) {
			var pointer = global::Json.Pointer.JsonPointer.Parse(operands[1].ConvertToString());
			var doc = JsonSerializer.SerializeToElement(operands[0]);
			return pointer.Evaluate(doc) ?? JsonDocument.Parse("null").RootElement;
		}
	}
}