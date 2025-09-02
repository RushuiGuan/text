namespace Albatross.Text.CliFormat {
	public enum Format {
		Auto,	// default, if the input is a collection, use Table, otherwise use Property
		Property,	// accept a single variable name as an optional parameter.  The variable name can be period delimited for nested properties.
		Table, 	// support multiple variable names as optional parameters.  The variable names can be period delimited for nested properties.
		Json,	// formatted json, support an optional single json pointer parameter
		JsonArray,	// treats the input as a collection.  Support 
		JsonScalar,	 // support an optional single json pointer parameter
		Csv,
	}

	public record class FormatOptions {
		public required Format Format { get; init; }
		public string[] Parameters { get; init; } = [];
	}
}