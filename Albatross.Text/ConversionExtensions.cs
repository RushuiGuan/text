using Albatross.Reflection;
using System;
using System.Collections.Generic;

namespace Albatross.Text {
	public static class ConversionExtensions {
		/// <summary>
		/// Convert text to an object of the specified type.  When the text is null or empty, the method will return default for value types
		/// and null for reference types except string.  If the type is string, it will return the text itself.  Note that this method will not
		/// return Nullable&lt;T&gt; when the type is Nullable&lt;T&gt;, it will always return T.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="type">Expected type</param>
		/// <returns></returns>
		/// <exception cref="NotSupportedException"></exception>
		/// <exception cref="FormatException"></exception>
		public static object? Convert(this string? text, Type type) {
			if (string.IsNullOrEmpty(text)) {
				if (type == typeof(string)) {
					return text;
				} else if (type.IsValueType) {
					return Activator.CreateInstance(type);
				} else {
					return null;
				}
			}
			if (type.IsEnum && Enum.TryParse(type, text, true, out var value)) {
				return value;
			}
			switch (Type.GetTypeCode(type)) {
				case TypeCode.SByte: return sbyte.Parse(text);
				case TypeCode.Byte: return byte.Parse(text);
				case TypeCode.Int16: return short.Parse(text);
				case TypeCode.UInt16: return ushort.Parse(text);
				case TypeCode.Int32: return int.Parse(text);
				case TypeCode.UInt32: return uint.Parse(text);
				case TypeCode.Int64: return long.Parse(text);
				case TypeCode.UInt64: return ulong.Parse(text);
				case TypeCode.String: return text;
				case TypeCode.DateTime: return DateTime.Parse(text);
				case TypeCode.Single: return float.Parse(text);
				case TypeCode.Double: return double.Parse(text);
				case TypeCode.Decimal: return decimal.Parse(text);
				case TypeCode.Boolean: return bool.Parse(text);
				case TypeCode.Char: return text[0];
			}
			if (type.GetNullableValueType(out var valueType)) {
				return Convert(text, valueType);
#if NET6_0_OR_GREATER
			} else if (type == typeof(DateOnly)) {
				return DateOnly.Parse(text);
			} else if (type == typeof(TimeOnly)) {
				return TimeOnly.Parse(text);
#endif
			} else {
				throw new NotSupportedException($"Cannot convert text \"{text}\" to type {type.Name}");
			}
		}

		/// <summary>
		/// Set the property value of an instance of type T using values from an instance of Dictionary&lt;string, string&gt;.
		/// The keys of the dictionary should match the property names of the type.  The value of the dictionary is converted
		/// to the type of the property using the <see cref="Convert(string?,System.Type)"/> method.
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="func"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Convert<T>(this IDictionary<string, string> dictionary, Func<T> func) where T : notnull {
			var obj = func();
			Convert(dictionary, typeof(T), obj);
			return obj;
		}

		public static object Convert(this IDictionary<string, string> dictionary, Type type, object obj) {
			foreach (var keyValuePair in dictionary) {
				var propertyType = type.GetPropertyType(keyValuePair.Key, true);
				var value = keyValuePair.Value.Convert(propertyType);
				type.SetPropertyValue(obj, keyValuePair.Key, value, true);
			}
			return obj;
		}
	}
}