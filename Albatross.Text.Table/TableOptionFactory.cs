using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Albatross.Text.Table {
	public class TableOptionFactory {
		object sync = new object();
		Dictionary<Type, TableOptions> registration = new Dictionary<Type, TableOptions>();

		public TableOptions<T> Get<T>() {
			var type = typeof(T);
			lock (sync) {
				if(!registration.TryGetValue(type, out var options)) {
					if (!TrySimpleValueCollectionRegistration<T>(out options)){
						options = FallBackRegistration<T>();
					}
					registration[typeof(T)] = options;
				}
				return (TableOptions<T>)options;
			}
		}

		public TableOptions Get(Type type) {
			var methodInfo = typeof(TableOptionFactory).GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.First(m => m.IsGenericMethod && m.Name == nameof(Get) && m.GetParameters().Length == 0)
				.MakeGenericMethod(type);
			return (TableOptions)methodInfo.Invoke(this, [])!;
		}
		public static TableOptionFactory Instance { get; } = new TableOptionFactory();
		public void Register<T>(TableOptions<T> options) => Register((TableOptions)options);
		public void Register(TableOptions options) {
			lock (sync) {
				registration[options.Type] = options;
			}
		}

		internal bool TrySimpleValueCollectionRegistration<T>([NotNullWhen(true)] out TableOptions? options) {
			var type = typeof(T);
			if(type.IsSimpleValue()){
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