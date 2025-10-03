using Albatross.Expression;
using Albatross.Expression.Parsing;
using Albatross.Reflection;
using Albatross.Serialization.Json;
using Albatross.Text.CliFormat.Operations;
using Albatross.Text.Table;
using System.Collections;
using System.Text.Json;

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
			builder.AddFactory(new PrefixExpressionFactory<Operations.JsonArray>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.JsonPointer>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Csv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.CompactCsv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Table>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.List>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.First>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Last>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Property>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.ArrayProperty>(false));
			return new Parser(builder.Factories, false);
		}

		/// <summary>
		/// Prints the specified value using a CLI format expression, with automatic format detection if no format is specified.
		/// </summary>
		/// <typeparam name="T">The type of the value to print.</typeparam>
		/// <param name="value">The value to format and print to console output.</param>
		/// <param name="format">The format expression to use. If null or empty, uses "auto(value)" for automatic format detection.</param>
		public static void CliPrint<T>(this TextWriter writer, T value, string? format) where T : notnull {
			object result;
			if (string.IsNullOrEmpty(format)) {
				result = value;
			} else {
				var parser = BuildCustomParser();
				var expr = parser.Build(format);
				var context = new CustomExecutionContext<T>(parser);
				result = expr.Eval(name => context.GetValue(name, value));
			}
			var type = result.GetType();
			if (result is StringTable stringTable) {
				stringTable.Print(writer);
			} else if (result is IDictionary dictionary) {
				dictionary.StringTable().Print(writer);
			} else if (type.TryGetGenericCollectionElementType(out var elementType)) {
				if (elementType == typeof(string) || elementType.IsPrimitive) {
					foreach (var item in (IEnumerable)result) {
						writer.WriteLine(item);
					}
				} else if (typeof(IDictionary).IsAssignableFrom(elementType)) {
					PrintDictionaryList((IEnumerable<IDictionary>)result, writer);
				} else {
					var options = TableOptionFactory.Instance.Get(elementType);
					new StringTable((IEnumerable)result, options).Print(writer);
				}
			} else if (result is JsonElement element) {
				writer.WriteLine(JsonSerializer.Serialize(element, FormattedJsonSettings.Instance.Value));
			} else {
				writer.WriteLine(result.ToString());
			}
		}

		static void PrintDictionaryList(IEnumerable<IDictionary> list, TextWriter writer) {
			StringTable? first = null;
			var tables = new List<StringTable>();
			foreach (var item in list) {
				var table = item.StringTable();
				tables.Add(table);
				if (first == null) {
					first = table;
				} else {
					first.Align(table, false);
				}
			}
			foreach (var table in tables) {
				if (table == first) {
					table.Print(writer, true, true, false);
				} else {
					table.Print(writer, false, false, true);
				}
			}
		}
	}
}