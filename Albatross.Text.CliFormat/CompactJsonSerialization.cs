using Albatross.Serialization.Json;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Provides JSON serialization settings configured for compact output without indentation, using camelCase naming.
	/// </summary>
	public class CompactJsonSerialization : IJsonSettings {
		/// <summary>
		/// Gets the JSON serializer options configured for compact output without indentation, camelCase naming, and default value exclusion.
		/// </summary>
		public JsonSerializerOptions Value { get; } = new() {
			WriteIndented = false,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};
		static readonly CompactJsonSerialization instance = new CompactJsonSerialization();
		/// <summary>
		/// Gets the singleton instance of the compact JSON settings.
		/// </summary>
		public static IJsonSettings Instance => instance;
	}
}