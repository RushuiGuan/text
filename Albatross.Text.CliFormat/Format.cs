namespace Albatross.Text.CliFormat {
	/// <summary>
	/// Specifies the output format type for CLI text formatting operations.
	/// </summary>
	public enum Format {
		/// <summary>
		/// Automatically selects the appropriate format based on input type. Uses Table format for collections, Property format for single objects.
		/// </summary>
		Auto,	// default, if the input is a collection, use Table, otherwise use Property
		/// <summary>
		/// Formats output as property-value pairs. Accepts a single variable name parameter supporting dot notation for nested properties.
		/// </summary>
		Property,	// accept a single variable name as an optional parameter.  The variable name can be period delimited for nested properties.
		/// <summary>
		/// Formats output as a table with columns. Supports multiple variable name parameters with dot notation for nested properties.
		/// </summary>
		Table, 	// support multiple variable names as optional parameters.  The variable names can be period delimited for nested properties.
		/// <summary>
		/// Formats output as indented JSON. Supports an optional JSON pointer parameter to extract specific data.
		/// </summary>
		Json,	// formatted json, support an optional single json pointer parameter
		/// <summary>
		/// Formats collections as JSON arrays with one item per line. Treats input as a collection.
		/// </summary>
		JsonArray,	// treats the input as a collection.  Support 
		/// <summary>
		/// Formats output as compact JSON scalars. Supports an optional JSON pointer parameter to extract specific data.
		/// </summary>
		JsonScalar,	 // support an optional single json pointer parameter
		/// <summary>
		/// Formats collections as comma-separated values with headers.
		/// </summary>
		Csv,
	}

	/// <summary>
	/// Configuration options for CLI text formatting operations.
	/// </summary>
	public record class FormatOptions {
		/// <summary>
		/// Gets the format type to use for output generation.
		/// </summary>
		public required Format Format { get; init; }
		/// <summary>
		/// Gets the parameters to pass to the formatter. Content depends on the selected format type.
		/// </summary>
		public string[] Parameters { get; init; } = [];
	}
}