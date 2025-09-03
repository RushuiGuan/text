using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using Json.Pointer;
using System.Text.RegularExpressions;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Represents a JSON pointer literal value that can be evaluated within expression contexts.
	/// </summary>
	public class JsonPointerLiteral : ValueToken, IExpression {
		/// <summary>
		/// Initializes a new instance of the JsonPointerLiteral class with the specified pointer value.
		/// </summary>
		/// <param name="value">The JSON pointer string value.</param>
		public JsonPointerLiteral(string value) : base(value) { }
		/// <summary>
		/// Evaluates the JSON pointer literal, returning the pointer value itself.
		/// </summary>
		/// <param name="context">The evaluation context (not used for literals).</param>
		/// <returns>The JSON pointer string value.</returns>
		public object Eval(Func<string, object> context) => Value;

		/// <summary>
		/// Asynchronously evaluates the JSON pointer literal, returning the pointer value itself.
		/// </summary>
		/// <param name="context">The evaluation context (not used for literals).</param>
		/// <returns>A task containing the JSON pointer string value.</returns>
		public Task<object> EvalAsync(Func<string, Task<object>> context)
			=> Task.FromResult(Eval(context));
	}

	/// <summary>
	/// Expression factory for parsing JSON pointer literals in format expressions.
	/// </summary>
	public class JsonPointerLiteralFactory : IExpressionFactory<JsonPointerLiteral> {
		/// <summary>
		/// Compiled regex pattern for matching JSON pointer syntax (paths starting with / followed by optional segments).
		/// </summary>
		public static readonly Regex Regex = new(@"^(?:/(?:[a-z_][a-z0-9_]*|\d+)?)+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		/// <summary>
		/// Attempts to parse a JSON pointer literal from the given text starting at the specified position.
		/// </summary>
		/// <param name="text">The text to parse.</param>
		/// <param name="start">The starting position in the text.</param>
		/// <param name="next">Outputs the position after the parsed literal, or the start position if parsing fails.</param>
		/// <returns>A JsonPointerLiteral instance if parsing succeeds, null otherwise.</returns>
		public JsonPointerLiteral? Parse(string text, int start, out int next) {
			return text.RegexParse(Regex, m => m.Value, x => new JsonPointerLiteral(x), start, out next);
		}
	}
}