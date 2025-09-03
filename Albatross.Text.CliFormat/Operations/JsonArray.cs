using Albatross.Expression;
using Albatross.Expression.Prefix;
using Json.Pointer;
using System.Text.Json;

namespace Albatross.Text.CliFormat.Operations {
	public class JsonArray : PrefixExpression {
		public JsonArray() : base("jsonarray", 1, int.MaxValue) {
		}

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