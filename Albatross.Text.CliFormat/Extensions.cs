using Albatross.Expression;
using Albatross.Expression.Parsing;
using Albatross.Text.CliFormat.Operations;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Provides extension methods and utilities for CLI text formatting operations.
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Builds a custom expression parser configured with factories for various text formatting operations.
		/// </summary>
		/// <returns>A configured parser instance that can evaluate CLI format expressions.</returns>
		public static IParser BuildCustomParser() {
			var builder = new ParserBuilder();
			builder.AddFactory(new BooleanLiteralFactory(false));
			builder.AddFactory(new NumericLiteralFactory());
			builder.AddFactory(StringLiteralFactory.DoubleQuote);
			builder.AddFactory(StringLiteralFactory.SingleQuote);
			builder.AddFactory(new CustomVariableFactory());
			builder.AddFactory(new JsonPointerLiteralFactory());
			builder.AddFactory(new PrefixExpressionFactory<Operations.Json>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Csv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.CompactCsv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.JsonArray>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Table>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.List>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.First>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Last>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Auto>(false));
			return new Parser(builder.Factories, false);
		}

		/// <summary>
		/// Prints the specified value using a CLI format expression, with automatic format detection if no format is specified.
		/// </summary>
		/// <typeparam name="T">The type of the value to print.</typeparam>
		/// <param name="value">The value to format and print to console output.</param>
		/// <param name="format">The format expression to use. If null or empty, uses "auto(value)" for automatic format detection.</param>
		public static void CliPrint<T>(this T value, string? format) where T : notnull {
			if (string.IsNullOrEmpty(format)) {
				format = "auto(value)";
			}
			var parser = BuildCustomParser();
			var expr = parser.Build(format);
			var context = new CustomExecutionContext<T>(parser);
			var result = expr.Eval(name => context.GetValue(name, value));
			Console.Out.WriteLine(result.ConvertToString());
		}
	}
}