using System.CommandLine.Parsing;

namespace Sample {
	class Program {
		static Task<int> Main(string[] args) {
			return new MySetup().AddCommands()
				.CommandBuilder.Build()
				.InvokeAsync(args);
		}
	}
}