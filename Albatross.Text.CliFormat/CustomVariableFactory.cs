using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using System.Text.RegularExpressions;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Expression factory for parsing variable names that support dot notation for accessing nested properties.
	/// </summary>
	public class CustomVariableFactory : IExpressionFactory<Variable> {
		// const string VariableNamePattern = @"^([a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)+)\b(?!\s*\()";

		/// <summary>
		/// Compiled regex pattern for matching variable names (supports dot notation like "obj.prop").
		/// </summary>
		public static readonly Regex VariableNameRegex = new Regex(@"^([a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)*)\b(?!\s*\()",
			RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		/// <summary>
		/// Attempts to parse a variable expression from the given text starting at the specified position.
		/// </summary>
		/// <param name="expression">The text expression to parse.</param>
		/// <param name="start">The starting position in the expression.</param>
		/// <param name="next">Outputs the position after the parsed variable, or the start position if parsing fails.</param>
		/// <returns>A Variable instance if parsing succeeds, null otherwise.</returns>
		public Variable? Parse(string expression, int start, out int next)
			=> expression.RegexParse(VariableNameRegex, 
				m => m.Groups[1].Value, 
				v => new Variable(v), start, out next);
	}
}