using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.Text.CliFormat.Operations {
	public class Help {
		public required string Name { get; init; } 
		public required string Alias { get; init; }
		public required string Description { get; init; }
	}
}
