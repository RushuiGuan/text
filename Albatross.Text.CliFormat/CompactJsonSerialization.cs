using Albatross.Serialization.Json;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	public class CompactJsonSerialization : IJsonSettings {
		public JsonSerializerOptions Value { get; } = new() {
			WriteIndented = false,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};
		static readonly CompactJsonSerialization instance = new CompactJsonSerialization();
		public static IJsonSettings Instance => instance;
	}
}