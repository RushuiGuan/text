using Albatross.Expression;
using Albatross.Expression.Prefix;
using CsvHelper.Configuration;
using System.Globalization;

namespace Albatross.Text.CliFormat.Operations {
	public class Csv : PrefixExpression {
		public Csv() : base("csv", 1, int.MaxValue) {
		}

		protected override object Run(List<object> operands) {
			return Print(operands, true);
		}
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
				var classMap = (ClassMap)Activator.CreateInstance(classMapType, [parameters]);
				using var writer = new CsvHelper.CsvWriter(textWriter, configuration, true);
				writer.Context.RegisterClassMap(classMap!);
				writer.WriteRecords(items);
			}
			return textWriter.ToString();
		}
	}
}
