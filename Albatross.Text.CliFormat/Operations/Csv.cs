using Albatross.Expression;
using Albatross.Expression.Prefix;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as CSV (comma-separated values) output with column headers.
	/// Supports custom column selection through additional parameters.
	/// </summary>
	public class Csv : PrefixExpression {
		/// <summary>
		/// Initializes the Csv operation supporting one or more operands.
		/// </summary>
		public Csv() : base("csv", 1, int.MaxValue) {
		}

		/// <summary>
		/// Executes CSV formatting with column headers.
		/// </summary>
		/// <param name="operands">The operands list where the first operand is the collection to format, and remaining operands specify column names.</param>
		/// <returns>A CSV formatted string with column headers.</returns>
		protected override object Run(List<object> operands) {
			return Print(operands, true);
		}

		/// <summary>
		/// Generates CSV output from a collection with configurable header inclusion and column selection.
		/// </summary>
		/// <param name="operands">The operands list where the first operand is the collection to format, and remaining operands specify column names.</param>
		/// <param name="showHeader">Indicates whether to include column headers in the output.</param>
		/// <returns>A CSV formatted string with or without headers based on the showHeader parameter.</returns>
		/// <remarks>
		/// When no column parameters are provided, all public properties are included.
		/// When column parameters are specified, only those properties are included in the specified order.
		/// </remarks>
		public static string Print(List<object> operands, bool showHeader) {
			var items = operands[0].ConvertToCollection(out var type);
			var parameters = operands.Skip(1).Select(x => x.ConvertToString()).ToArray();
			var textWriter = new StringWriter();
			var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) {
				HasHeaderRecord = showHeader,
			};
			if (parameters.Length == 0) {
				using var writer = new CsvHelper.CsvWriter(textWriter, configuration, true);
				writer.WriteRecords(items);
			} else {
				var classMapType = typeof(CsvClassMap<>).MakeGenericType(type);
				ClassMap classMap;
				try {
					classMap = (ClassMap)Activator.CreateInstance(classMapType, [parameters])!;
				} catch (TargetInvocationException ex) when (ex.InnerException is ArgumentException) {
					throw new InvalidOperationException(ex.InnerException.Message);
				} catch {
					throw new InvalidOperationException($"Unable to create a csv class map for {type.Name}");
				}
				using var writer = new CsvHelper.CsvWriter(textWriter, configuration, true);
				writer.Context.RegisterClassMap(classMap!);
				writer.WriteRecords(items);
			}
			return textWriter.ToString().TrimEnd('\r', '\n');
		}
	}
}
