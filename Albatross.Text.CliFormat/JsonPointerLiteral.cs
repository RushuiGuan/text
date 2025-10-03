using Albatross.Expression.Nodes;
using Json.Pointer;

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
}