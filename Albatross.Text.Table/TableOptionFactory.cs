using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Albatross.Text.Table {
	/// <summary>
	/// Thread-safe singleton factory for managing and caching <see cref="TableOptions"/> configurations.
	/// Types are auto-registered on first use via reflection-based column discovery.
	/// </summary>
	public class TableOptionFactory {
		readonly object sync = new object();
		readonly Dictionary<Type, TableOptions> registration = new Dictionary<Type, TableOptions>();

		/// <summary>
		/// Attempts to retrieve a previously registered configuration for type T.
		/// </summary>
		/// <returns>True if a configuration was found; otherwise false.</returns>
		public bool TryGet<T>([NotNullWhen(true)] out TableOptions<T>? options) {
			var type = typeof(T);
			lock (sync) {
				if (registration.TryGetValue(type, out var found)) {
					options = (TableOptions<T>)found;
					return true;
				}
			}
			options = null;
			return false;
		}

		/// <summary>
		/// Gets or creates a table configuration for type T. Auto-registers using reflection if not previously configured.
		/// </summary>
		public TableOptions<T> Get<T>() {
			var type = typeof(T);
			lock (sync) {
				if (!registration.TryGetValue(type, out var options)) {
					if (!TrySimpleValueCollectionRegistration<T>(out options)) {
						options = FallBackRegistration<T>();
					}
					registration[typeof(T)] = options;
				}
				return (TableOptions<T>)options;
			}
		}

		/// <summary>
		/// Gets or creates a table configuration for the specified type using reflection.
		/// </summary>
		public TableOptions Get(Type type) {
			var methodInfo = typeof(TableOptionFactory).GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.First(m => m.IsGenericMethod && m.Name == nameof(Get) && m.GetParameters().Length == 0)
				.MakeGenericMethod(type);
			return (TableOptions)methodInfo.Invoke(this, [])!;
		}

		/// <summary>
		/// Global singleton instance for application-wide table option management.
		/// </summary>
		public static TableOptionFactory Instance { get; } = new TableOptionFactory();

		/// <summary>
		/// Registers a custom table configuration for type T, overwriting any existing configuration.
		/// </summary>
		public void Register<T>(TableOptions<T> options) => Register((TableOptions)options);

		/// <summary>
		/// Registers a custom table configuration, overwriting any existing configuration for that type.
		/// </summary>
		public void Register(TableOptions options) {
			lock (sync) {
				registration[options.Type] = options;
			}
		}

		internal bool TrySimpleValueCollectionRegistration<T>([NotNullWhen(true)] out TableOptions? options) {
			var type = typeof(T);
			if (type.IsSimpleValue()) {
				options = new TableOptions<T>()
					.SetColumn<T>("Value", x => x)
					.PrintFirstLineSeparator(false)
					.PrintLastLineSeparator(false)
					.PrintHeader(false);
				return true;
			} else {
				options = null;
				return false;
			}
		}

		internal TableOptions FallBackRegistration<T>() {
			return new TableOptions<T>().BuildColumnsByReflection();
		}
	}
}