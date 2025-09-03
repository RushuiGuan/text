using CsvHelper.Configuration;
using System.Reflection;

namespace Albatross.Text.CliFormat {
	public class CsvClassMap<T> : ClassMap<T> {
		public CsvClassMap(string[] parameters) {
			var type = typeof(T);
			foreach (var param in parameters) {
				Map(type, type.GetProperty(param, BindingFlags.Instance | BindingFlags.Public) ?? throw new ArgumentException($"Property '{param}' not found on type '{typeof(T).Name}'"));
			}
		}
	}
}