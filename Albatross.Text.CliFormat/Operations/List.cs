using Albatross.Expression;
using Albatross.Expression.Prefix;
using Albatross.Reflection;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat.Operations {
	public class List : PrefixExpression {
		public List() : base("list", 1, 2) {
		}

		protected override object Run(List<object> operands) {
			var list = operands[0].ConvertToCollection(out var type);
			string? column = null;
			if(operands.Count > 1) {
				column = operands[1].ConvertToString();
			}
			return Print(list, type, null, column, false);
		}
		public static object Print(IEnumerable<object> value, Type type, int? count, string? column, bool reversed) {
			if (count.HasValue) {
				if (reversed) {
					value = value.TakeLast(count.Value);
				} else {
					value = value.TakeLast(count.Value);
				}
			}
			var writer = new StringWriter();
			foreach (var item in value) {
				var target = item;
				if (!string.IsNullOrEmpty(column)) {
					target = type.GetPropertyValue(item, column, true);
				}
				if (target == null) {
					writer.AppendLine(string.Empty);
				} else if (target.GetType().IsPrimitive || target is string) {
					writer.AppendLine(target);
				} else {
					var stringTable = target.PropertyTable(null, null);
					stringTable.Print(writer);
				}
			}
			return writer.ToString();
		}
	}
}