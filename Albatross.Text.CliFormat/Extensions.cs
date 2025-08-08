using System.Text.Json;

namespace Albatross.Text.CliFormat;
using Albatross.Text.Table;

public static class Extensions {
	public static string CliPrint<T>(this IEnumerable<T> items, string format) {
		var writer = new StringWriter();
		switch (format) {
			case "table":
				items.StringTable().Print(writer);
				break;
			case "json":
				writer.WriteLine(JsonSerializer.Serialize<IEnumerable<T>>(items, DefaultCliJsonSerialization.Instance.Value));
				break;
		}
		return writer.ToString();
	}
}