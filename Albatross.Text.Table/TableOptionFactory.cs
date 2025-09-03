using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albatross.Text.Table {
	public class TableOptionFactory {
		object sync = new object();
		Dictionary<Type, TableOptions> registration = new Dictionary<Type, TableOptions>();

		public TableOptions<T> Get<T>() {
			lock (sync) {
				if (registration.TryGetValue(typeof(T), out TableOptions? options)) {
					return (TableOptions<T>)options;
				} else {
					var newOptions = new TableOptionBuilder<T>().GetColumnBuildersByReflection().Build();
					Register<T>(newOptions);
					return newOptions;
				}
			}
		}

		public TableOptions Get(Type type) {
			lock (sync) {
				if (registration.TryGetValue(type, out TableOptions? options)) {
					return options;
				} else {
					return new TableOptions(type) {
						ColumnOptions = type.GetColumnsByReflection().ToArray(),
					};
				}
			}
		}
		
		public static TableOptionFactory Instance { get; } = new TableOptionFactory();
		public void Register<T>(TableOptions<T> options) => Register((TableOptions)options);
		public void Register(TableOptions options) {
			lock (sync) {
				registration[options.Type] = options;
			}
		}
	}
}