using CsvHelper.Configuration;

namespace Albatross.Text.CliFormat {
	public class CsvClassMap<T> : ClassMap<T> {
		public CsvClassMap(string[] parameters) {
			foreach (var param in parameters) {
				Map(typeof(T), typeof(T).GetProperty(param) ?? throw new ArgumentException($"Property '{param}' not found on type '{typeof(T).Name}'"));
			}
		}
	}
}