using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.Text.CliFormat.Operations {
	public class Help {
		public required string Name { get; init; }
		public required string Description { get; init; }

		public static readonly Help[] All = new Help[] {
			new Help { Name = "table", Description = "Print as a table" },
			new Help { Name = "json", Description = "Take builtin value as its only parameter.  The operation serializes the value into a JsonElement with camel case naming convention" },
			new Help { Name = "jsonpointer", Description = "Expects the builtin value as its first parameter, a join pointer expression as its second.  The operation will return the value at the specified JSON pointer location." },
			new Help { Name = "csv", Description = "Print as CSV" },
			new Help { Name = "tsv", Description = "Print as TSV" },
			new Help { Name = "props", Description = "Print as properties (key=value)" },
			new Help { Name = "props-quote", Description = "Print as properties with quotes (key=\"value\")" },
			new Help { Name = "xml", Description = "Print as XML" },
			new Help { Name = "html", Description = "Print as HTML" },
		};
	}
}
