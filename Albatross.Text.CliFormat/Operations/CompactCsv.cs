using Albatross.Expression.Prefix;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Formats collections as compact CSV (comma-separated values) output without column headers.
	/// </summary>
	public class CompactCsv: PrefixExpression {
		/// <summary>
		/// Initializes the CompactCsv operation supporting one or more operands.
		/// </summary>
		public CompactCsv() : base("ccsv", 1, int.MaxValue) {
		}

		/// <summary>
		/// Executes the compact CSV formatting operation without headers.
		/// </summary>
		/// <param name="operands">The operands list where the first operand is the collection to format, and remaining operands specify column names.</param>
		/// <returns>A CSV formatted string without column headers.</returns>
		protected override object Run(List<object> operands) {
			return Csv.Print(operands, false);
		}
	}
}
