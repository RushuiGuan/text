using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using System.Text.RegularExpressions;

namespace Albatross.Text.CliFormat {
	public class CustomVariableFactory : IExpressionFactory<Variable> {
		const string VariableNamePattern = @"^([a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)+) \b (?!\s*\() ";

		/// <summary>
		/// Compiled regex pattern for matching variable names (supports dot notation like "obj.prop").
		/// </summary>
		public static readonly Regex VariableNameRegex = new Regex(VariableNamePattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		public Variable? Parse(string expression, int start, out int next)
			=> expression.RegexParse(VariableNameRegex, 
				m => m.Groups[1].Value, 
				v => new Variable(v), start, out next);
	}
}