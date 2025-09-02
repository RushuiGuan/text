using Albatross.Serialization.Json;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	public class FormattedJsonSerialization : IJsonSettings {
		public JsonSerializerOptions Value { get; } = new() {
			WriteIndented = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};
		static readonly FormattedJsonSerialization instance = new FormattedJsonSerialization();
		public static IJsonSettings Instance => instance;
	}
}