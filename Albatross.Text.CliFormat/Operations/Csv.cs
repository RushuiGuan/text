using Albatross.Expression;
using Albatross.Expression.Prefix;
using CsvHelper.Configuration;
using System.Globalization;

namespace Albatross.Text.CliFormat.Operations {
	public class Csv : PrefixExpression {
		public Csv() : base("csv", 1, int.MaxValue) {
		}

		protected override object Run(List<object> operands) {
			var items = operands[0].ConvertToCollection(out var type);
			var parameters = operands.Skip(1).Select(x => x.ConvertToString()).ToArray();
			var textWriter = new StringWriter();
			if (parameters.Length == 0) {
				var writer = new CsvHelper.CsvWriter(textWriter, CultureInfo.InvariantCulture, true);
				writer.WriteRecords(items);
			} else {
				var classMapType = typeof(CsvClassMap<>).MakeGenericType(type);
				var classMap = (ClassMap)Activator.CreateInstance(classMapType, parameters);
				var writer = new CsvHelper.CsvWriter(textWriter, CultureInfo.InvariantCulture, true);
				writer.Context.RegisterClassMap(classMap!);
				writer.WriteRecords(items);
			}
			return 0;
		}
	}
}
