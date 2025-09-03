using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	public class CompactCsv: PrefixExpression {
		public CompactCsv() : base("ccsv", 1, int.MaxValue) {
		}

		protected override object Run(List<object> operands) {
			return Csv.Print(operands, false);
		}
	}
}
