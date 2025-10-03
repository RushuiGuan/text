using Albatross.Expression.Parsing;
using System.Text.RegularExpressions;

namespace Albatross.Text.CliFormat {
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