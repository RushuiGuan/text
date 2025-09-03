using Albatross.Expression;
using Albatross.Expression.Parsing;
using Albatross.Text.CliFormat.Operations;
using Albatross.Text.Table;

namespace Albatross.Text.CliFormat {
	public static class Extensions {
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