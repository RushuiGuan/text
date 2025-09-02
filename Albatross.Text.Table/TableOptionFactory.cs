using System;
using System.Collections.Generic;
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
					var newOptions = new TableOptionBuilder<T>().SetColumnsByReflection().Build();
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
					var columnOptions = new List<TableColumnOption>();
					int index = 0;
					foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
						if (property.GetIndexParameters().Length > 0) {
							// Skip indexers
							continue;
						}
						int order = index++;
						var columnOption = new TableColumnOption(x => property.GetValue(x), (_, value) => new TextValue(DefaultFormat(value))) {
							Header = property.Name,
							Order = order,
							Property = property.Name,
						};
						columnOptions.Add(columnOption);
					}
					return new TableOptions {
						ColumnOptions = columnOptions.ToArray()
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