using Albatross.Serialization.Json;
using System.Text.Json;

namespace Albatross.Text.CliFormat {
	public class DefaultCliJsonSerialization : Albatross.Serialization.Json.IJsonSettings {
		public JsonSerializerOptions Value { get; } = new() {
			WriteIndented = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
		};
		static readonly DefaultCliJsonSerialization instance = new DefaultCliJsonSerialization();
		public static IJsonSettings Instance => instance;
	}
}