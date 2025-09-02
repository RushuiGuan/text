using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using Json.Pointer;
using System.Text.RegularExpressions;

namespace Albatross.Text.CliFormat {
	public class JsonPointerLiteral : ValueToken, IExpression {
		public JsonPointerLiteral(string value) : base(value) { }
		public object Eval(Func<string, object> context) => Value;

		public Task<object> EvalAsync(Func<string, Task<object>> context)
			=> Task.FromResult(Eval(context));
	}

	public class JsonPointerLiteralFactory : IExpressionFactory<JsonPointerLiteral> {
		public static readonly Regex Regex = new(@"^(?:/(?:[a-z_][a-z0-9_]*|\d+)?)+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		public JsonPointerLiteral? Parse(string text, int start, out int next) {
			return text.RegexParse(Regex, m => m.Value, x => new JsonPointerLiteral(x), start, out next);
		}
	}
}