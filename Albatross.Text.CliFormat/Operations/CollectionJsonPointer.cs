using Albatross.Expression;
using Albatross.Expression.Prefix;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class CollectionJsonPointer : PrefixExpression {
		public CollectionJsonPointer() : base("cjsonpointer", 2, 2) {
		}

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