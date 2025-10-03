using Albatross.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Albatross.Text.Table {
	public static class GenericTableOptionsExtension {
		public static TableOptions<T> Format<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, string format)
			=> options.Format(lambda, (T entity, object? value) => new TextValue(string.Format($"{{0:{format}}}", value)));

		public static TableOptions<T> Format<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object?, TextValue> format) {
			options.Format(lambda.GetPropertyInfo().Name, (entity, value) => format((T)entity, value));
			return options;
		}

		public static TableOptions<T> ColumnOrder<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, int order) {
			options.ColumnOrder(lambda.GetPropertyInfo().Name, order);
			return options;
		}

		public static TableOptions<T> ColumnHeader<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, string newHeader) {
			options.ColumnHeader(lambda.GetPropertyInfo().Name, newHeader);
			return options;
		}

		public static TableOptions<T> Ignore<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda) {
			options.Ignore(lambda.GetPropertyInfo().Name);
			return options;
		}

		public static TableOptions<T> SetColumn<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object?>? getValue = null) {
			var propertyInfo = lambda.GetPropertyInfo();
			if (getValue == null) {
				getValue = x => propertyInfo.GetValue(x);
			}
			options.SetColumn(propertyInfo.Name, (entity) => getValue((T)entity));
			return options;
		}


		public static TableOptions BuildColumnsByReflection(this TableOptions options) {
			foreach (var property in options.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
				if (property.GetIndexParameters().Length > 0) {
					// Skip indexers
					continue;
				}
				options.SetColumn(property.Name, x => property.GetValue(x));
				options.Format(property.Name, (_, value) => new TextValue(TableOptions.DefaultFormat(value)));
				options.ColumnHeader(property.Name, property.Name);
			}
			return options;
		}

		public static TableOptions<T> SetColumn<T>(this TableOptions<T> options, string column) where T : System.Collections.IDictionary {
			options.SetColumn(column, t => ((IDictionary)t)[column]);
			return options;
		}
	}
}