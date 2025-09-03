using Albatross.Serialization.Json;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Provides JSON serialization settings configured for readable, formatted output with indentation and camelCase naming.
	/// </summary>
	public class FormattedJsonSerialization : IJsonSettings {
		/// <summary>
		/// Gets the JSON serializer options configured for formatted output with indentation, camelCase naming, and default value exclusion.
		/// </summary>
		public JsonSerializerOptions Value { get; } = new() {
			WriteIndented = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};
		static readonly FormattedJsonSerialization instance = new FormattedJsonSerialization();
		/// <summary>
		/// Gets the singleton instance of the formatted JSON settings.
		/// </summary>
		public static IJsonSettings Instance => instance;
	}
}