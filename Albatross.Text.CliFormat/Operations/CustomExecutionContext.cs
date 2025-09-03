using Albatross.Expression;
using Albatross.Expression.Context;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Custom execution context that provides a built-in "value" variable and dynamic property access for CLI formatting expressions.
	/// </summary>
	/// <typeparam name="T">The type of the value being processed in the execution context.</typeparam>
	public class CustomExecutionContext<T> : ExecutionContext<T> where T : notnull {
		/// <summary>
		/// Initializes a new custom execution context with the specified parser.
		/// </summary>
		/// <param name="parser">The expression parser to use for evaluating expressions.</param>
		public CustomExecutionContext(IParser parser) : base(parser) {
		}
		ExternalContextValue<T> builtInValue = new ExternalContextValue<T>("value", x => x);

		/// <summary>
		/// Attempts to resolve a variable name to a context value handler.
		/// Returns the built-in "value" variable for the reserved name, otherwise creates a local context value for property access.
		/// </summary>
		/// <param name="name">The variable name to resolve.</param>
		/// <param name="value">When successful, contains the context value handler for the specified name.</param>
		/// <returns>Always returns true, as this context supports dynamic variable resolution.</returns>
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
