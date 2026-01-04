using Albatross.Expression;
using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using Albatross.Reflection;
using Albatross.Text.CliFormat.Operations;
using Albatross.Text.Table;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Provides extension methods and utilities for CLI text formatting operations.
	/// </summary>
	public static class Extensions {
		public readonly static Lazy<IParser> DefaultParser = new(() => BuildCustomParser());
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
			builder.AddFactory(new PrefixExpressionFactory<Operations.CollectionJsonPointer>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.JsonPointer>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Csv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.CompactCsv>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Table>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.List>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.First>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Last>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Property>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.CollectionProperty>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.Subset>(false));
			builder.AddFactory(new PrefixExpressionFactory<Operations.CollectionFormat>(false));
			return new Parser(builder.Factories, false);
		}

		readonly static JsonSerializerOptions jsonSerializerOptions = new() {
			WriteIndented = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};

		[return: NotNullIfNotNull(nameof(format))]
		public static IExpression? CreateExpression(string? format) {
			if (string.IsNullOrEmpty(format)) {
				return null;
			} else {
				return DefaultParser.Value.Build(format);
			}
		}

		/// <summary>
		/// Prints the specified value using a CLI format expression, with automatic format detection if no format is specified.
		/// </summary>
		/// <typeparam name="T">The type of the value to print.</typeparam>
		/// <param name="writer"></param>
		/// <param name="value">The value to format and print to console output.</param>
		/// <param name="format">The format expression to use. If null or empty, uses "auto(value)" for automatic format detection.</param>
		public static TextWriter CliPrint<T>(this TextWriter writer, T value, string? format) where T : notnull {
			var expression = CreateExpression(format);
			return CliPrintWithExpression(writer, value, expression);
		}

		public static TextWriter CliPrintWithExpression<T>(this TextWriter writer, T value, IExpression? expression) where T : notnull {
			object result;
			if (expression == null) {
				result = value;
			} else {
				var context = new CustomExecutionContext<T>(DefaultParser.Value);
				result = expression.Eval(name => context.GetValue(name, value));
			}
			return writer.Print<T>(result);
		}

		public static TextWriter Print<T>(this TextWriter writer, object value) where T : notnull {
			var type = value.GetType();
			if (type.IsSimpleValue()) {
				writer.WriteLine(TableOptions.DefaultFormat(value));
			} else if (value is StringTable stringTable) {
				stringTable.Print(writer);
			} else if (type.TryGetGenericCollectionElementType(out var elementType)) {
				if (typeof(IDictionary).IsAssignableFrom(elementType)) {
					PrintDictionaryList((IEnumerable<IDictionary>)value, writer);
				} else {
					var options = TableOptionFactory.Instance.Get(elementType);
					new StringTable((IEnumerable)value, options).Print(writer);
				}
			} else if (value is JsonElement element) {
				writer.WriteLine(JsonSerializer.Serialize(element, jsonSerializerOptions));
			} else {
				var dictionary = new Dictionary<string, object>();
				value.ToDictionary(dictionary);
				dictionary.StringTable().Print(writer);
			}
			return writer;
		}


		static void PrintDictionaryList(IEnumerable<IDictionary> list, TextWriter writer) {
			var tables = new List<StringTable>();
			StringTable? first = null;
			foreach (var item in list) {
				var table = item.StringTable();
				if (first == null) {
					first = table;
				}
				tables.Add(table);
			}
			tables.AlignAll();
			foreach (var table in tables) {
				if (table == first) {
					table.Print(writer, true, true, true);
				} else {
					table.Print(writer, false, false, true);
				}
			}
		}
	}
}