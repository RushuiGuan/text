using Albatross.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Albatross.Text.Table {
	/// <summary>
	/// Expression-based extension methods for configuring <see cref="TableOptions{T}"/> with compile-time property safety.
	/// </summary>
	public static class GenericTableOptionsExtension {
		/// <summary>
		/// Sets a .NET format string for a column identified by a property expression.
		/// </summary>
		public static TableOptions<T> Format<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, string format)
			=> options.Format(lambda, (T entity, object? value) => new TextValue(string.Format($"{{0:{format}}}", value)));

		/// <summary>
		/// Sets a custom formatter for a column identified by a property expression.
		/// </summary>
		public static TableOptions<T> Format<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object?, TextValue> format) {
			options.Format(lambda.GetPropertyInfo().Name, (entity, value) => format((T)entity, value));
			return options;
		}

		/// <summary>
		/// Sets the display order for a column identified by a property expression.
		/// </summary>
		public static TableOptions<T> ColumnOrder<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, int order) {
			options.ColumnOrder(lambda.GetPropertyInfo().Name, order);
			return options;
		}

		/// <summary>
		/// Changes the header text for a column identified by a property expression.
		/// </summary>
		public static TableOptions<T> ColumnHeader<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, string newHeader) {
			options.ColumnHeader(lambda.GetPropertyInfo().Name, newHeader);
			return options;
		}

		/// <summary>
		/// Removes a column identified by a property expression from the table output.
		/// </summary>
		public static TableOptions<T> Ignore<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda) {
			options.Ignore(lambda.GetPropertyInfo().Name);
			return options;
		}

		/// <summary>
		/// Adds or updates a column identified by a property expression with an optional custom value getter.
		/// </summary>
		public static TableOptions<T> SetColumn<T, P>(this TableOptions<T> options, Expression<Func<T, P>> lambda, Func<T, object?>? getValue = null) {
			var propertyInfo = lambda.GetPropertyInfo();
			if (getValue == null) {
				getValue = x => propertyInfo.GetValue(x);
			}
			options.SetColumn(propertyInfo.Name, (entity) => getValue((T)entity));
			return options;
		}


		/// <summary>
		/// Automatically discovers and configures columns from all public instance properties of type T.
		/// </summary>
		public static TableOptions<T> BuildColumnsByReflection<T>(this TableOptions<T> options) {
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

		/// <summary>
		/// Adds a column for dictionary types that extracts values by key.
		/// </summary>
		public static TableOptions<T> SetColumn<T>(this TableOptions<T> options, string column) where T : System.Collections.IDictionary {
			options.SetColumn(column, t => ((IDictionary)t)[column]);
			return options;
		}
	}
}