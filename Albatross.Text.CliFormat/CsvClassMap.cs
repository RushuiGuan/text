using CsvHelper.Configuration;
using System.Reflection;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Configures CSV column mapping for a specific type based on provided property names.
	/// </summary>
	/// <typeparam name="T">The type to create CSV mappings for.</typeparam>
	public class CsvClassMap<T> : ClassMap<T> {
		/// <summary>
		/// Initializes a new CSV class map that maps only the specified properties from type T.
		/// </summary>
		/// <param name="parameters">The property names to include in the CSV mapping.</param>
		/// <exception cref="ArgumentException">Thrown when a specified property name is not found on type T.</exception>
		public CsvClassMap(string[] parameters) {
			var type = typeof(T);
			foreach (var param in parameters) {
				Map(type, type.GetProperty(param, BindingFlags.Instance | BindingFlags.Public) ?? throw new ArgumentException($"Property '{param}' not found on type '{typeof(T).Name}'"));
			}
		}
	}
}