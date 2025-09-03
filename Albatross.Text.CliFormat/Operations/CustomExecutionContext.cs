using Albatross.Expression;
using Albatross.Expression.Context;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.Text.CliFormat.Operations {
	public class CustomExecutionContext<T> : ExecutionContext<T> where T : notnull {
		public CustomExecutionContext(IParser parser) : base(parser) {
		}
		ExternalContextValue<T> builtInValue = new ExternalContextValue<T>("value", x => x);

		protected override bool TryGetValueHandler(string name, [NotNullWhen(true)] out IContextValue<T>? value) {
			if (string.Equals(name, builtInValue.Name, StringComparison.OrdinalIgnoreCase)) {
				value = this.builtInValue;
				return true;
			} else {
				value = new LocalContextValue<T>(name, name);
				return true;
			}
		}
	}
}
