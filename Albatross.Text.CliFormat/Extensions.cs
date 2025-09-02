using Albatross.Expression;
using Albatross.Expression.Nodes;
using Albatross.Expression.Parsing;
using Albatross.Expression.Prefix;
using System.Text.Json;
using Albatross.Text.Table;
using Json.Pointer;
using System.Globalization;

namespace Albatross.Text.CliFormat {
	public static class Extensions {
		public static IParser BuildCustomParser() {
			var builder = new ParserBuilder();
			builder.AddFactory(new BooleanLiteralFactory(caseSensitive));
			builder.AddFactory(new NumericLiteralFactory());
			builder.AddFactory(StringLiteralFactory.DoubleQuote);
			builder.AddFactory(StringLiteralFactory.SingleQuote);
			builder.AddFactory(new CustomVariableFactory());
			builder.AddFactory(new JsonPointerLiteralFactory());
			builder.AddGenericPrefixFactory(false);
			return new Parser(builder.Factories, false);
		}

		public static FormatOptions GetFormatOptions(this string? text) {
			if (string.IsNullOrEmpty(text)) {
				return new FormatOptions { Format = Format.Auto };
			} else {
				var parser = new ParserBuilder().BuildDefault();
				var expr = parser.Build(text);
				if (expr is PrefixExpression prefix) {
					if (Enum.TryParse<Format>(prefix.Name, true, out var format)) {
						var parameters = prefix.Operands.Select(args => args.Eval(x => x).ConvertToString()).ToArray();
						return new FormatOptions {
							Format = format,
							Parameters = parameters
						};
					} else {
						throw new ArgumentException($"Invalid format: {text}");
					}
				} else if (expr is Variable variable) {
					if (Enum.TryParse<Format>(variable.Value, true, out var format)) {
						return new FormatOptions {
							Format = format,
							Parameters = []
						};
					} else {
						throw new ArgumentException($"Invalid format: {text}");
					}
				} else {
					throw new ArgumentException($"Invalid format: {text}");
				}
			}
		}

		public static void CliPrintItems<T>(FormatOptions options, params IEnumerable<T> items) {
			switch (options.Format) {
				case Format.JsonArray:
					PrintJsonScalar(items);
					break;
				case Format.JsonScalar:
					PrintJsonCollection(items, options.Parameters);
					break;
				case Format.Table:
					PrintCollectionByTable(items, options.Parameters);
					break;
				case Format.Csv:
					PrintCollectionByCsv(items, options.Parameters);
					break;
				default:
					throw new ArgumentException($"Unsupported format: {options.Format}");
			}
		}

		private static void PrintValueJson<T>(T item, string[] parameters) {
			if (parameters.Length == 0) {
				var text = JsonSerializer.Serialize(item, CompactJsonSerialization.Instance.Value);
				Console.WriteLine(text);
			} else {
			}
		}

		private static void PrintJsonScalar<T>(IEnumerable<T> items) {
			items.StringTable().PrintConsole();
		}
		private static void PrintJsonCollection<T>(IEnumerable<T> items, string[] parameters) {
			if (parameters.Length == 0) {
				var text = JsonSerializer.Serialize(items, FormattedJsonSerialization.Instance.Value);
				Console.WriteLine(text);
			} else {
				var pointers = parameters.Select(x=> JsonPointer.Create(x)).ToArray();
				foreach (var item in items) {
					var doc = JsonSerializer.SerializeToElement(item, CompactJsonSerialization.Instance.Value);
					Console.Out.WriteItems(pointers, ",", (w, pointer) => {
						var result = pointer.Evaluate(doc);
						var text = JsonSerializer.Serialize(result, CompactJsonSerialization.Instance.Value);
						w.Write(text);
					});
					Console.Out.WriteLine();
				}
			}
		}
		private static void PrintCollectionByTable<T>(IEnumerable<T> items, string[] parameters) {
			if (parameters.Length == 0) {
				items.StringTable().Print(Console.Out);
			} else {
				items.StringTable().PrintColumns(Console.Out, parameters, ",");
			}
		}
		
		private static void PrintCollectionByCsv<T>(IEnumerable<T> items, string[] parameters) {
			if(parameters.Length == 0) {
				var writer = new CsvHelper.CsvWriter(Console.Out, CultureInfo.InvariantCulture, true);
				writer.WriteRecords(items);
			} else {
				var classMap = new CsvClassMap<T>(parameters);
				var writer = new CsvHelper.CsvWriter(Console.Out, CultureInfo.InvariantCulture, true);
				writer.Context.RegisterClassMap(classMap);
				writer.WriteRecords(items);
			}
		}
	}
}