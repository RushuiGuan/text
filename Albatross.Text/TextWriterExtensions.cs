using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.Text {
	/// <summary>
	/// Fluent extension methods for <see cref="TextWriter"/> that enable chained write operations and common formatting tasks.
	/// </summary>
	public static partial class TextWriterExtensions {
		/// <summary>
		/// Writes the object to the TextWriter and returns the writer for method chaining.
		/// </summary>
		public static TextWriter Append(this TextWriter writer, object obj) {
			writer.Write(obj);
			return writer;
		}

		/// <summary>
		/// Conditionally writes the object to the TextWriter based on the specified condition.
		/// </summary>
		public static TextWriter AppendIf(this TextWriter writer, bool condition, object obj) {
			if (condition) {
				writer.Write(obj);
			}
			return writer;
		}

		/// <summary>
		/// Writes a boolean value as 1 (true) or 0 (false).
		/// </summary>
		public static TextWriter AppendBooleanAsBit(this TextWriter writer, bool value) {
			if (value) {
				writer.Write(1);
			} else {
				writer.Write(0);
			}
			return writer;
		}

		/// <summary>
		/// Writes a line terminator to the TextWriter.
		/// </summary>
		public static TextWriter AppendLine(this TextWriter writer) {
			writer.WriteLine();
			return writer;
		}

		/// <summary>
		/// Writes the object followed by a line terminator to the TextWriter.
		/// </summary>
		public static TextWriter AppendLine(this TextWriter writer, object obj) {
			writer.WriteLine(obj);
			return writer;
		}

		/// <summary>
		/// Writes the specified character one or more times.
		/// </summary>
		public static TextWriter AppendChar(this TextWriter writer, char c, int count = 1) {
			for (int i = 0; i < count; i++) {
				writer.Write(c);
			}
			return writer;
		}

		/// <summary>Writes one or more tab characters.</summary>
		public static TextWriter Tab(this TextWriter writer, int count = 1) {
			return writer.AppendChar('\t', count);
		}

		/// <summary>Writes one or more dot characters.</summary>
		public static TextWriter Dot(this TextWriter writer, int count = 1) {
			return writer.AppendChar('.', count);
		}

		/// <summary>Writes one or more comma characters.</summary>
		public static TextWriter Comma(this TextWriter writer, int count = 1) {
			return writer.AppendChar(',', count);
		}

		/// <summary>Writes one or more '[' characters.</summary>
		public static TextWriter OpenSquareBracket(this TextWriter writer, int count = 1) {
			return writer.AppendChar('[', count);
		}

		/// <summary>Writes one or more ']' characters.</summary>
		public static TextWriter CloseSquareBracket(this TextWriter writer, int count = 1) {
			return writer.AppendChar(']', count);
		}

		/// <summary>Writes one or more '&lt;' characters.</summary>
		public static TextWriter OpenAngleBracket(this TextWriter writer, int count = 1) {
			return writer.AppendChar('<', count);
		}

		/// <summary>Writes one or more '&gt;' characters.</summary>
		public static TextWriter CloseAngleBracket(this TextWriter writer, int count = 1) {
			return writer.AppendChar('>', count);
		}

		/// <summary>Writes one or more '(' characters.</summary>
		public static TextWriter OpenParenthesis(this TextWriter writer, int count = 1) {
			return writer.AppendChar('(', count);
		}

		/// <summary>Writes one or more ')' characters.</summary>
		public static TextWriter CloseParenthesis(this TextWriter writer, int count = 1) {
			return writer.AppendChar(')', count);
		}

		/// <summary>Writes one or more space characters.</summary>
		public static TextWriter Space(this TextWriter writer, int count = 1) {
			return writer.AppendChar(' ', count);
		}

		/// <summary>Writes one or more semicolon characters.</summary>
		public static TextWriter Semicolon(this TextWriter writer, int count = 1) {
			return writer.AppendChar(';', count);
		}

		/// <summary>
		/// this signature is added for backward compatibility reasons
		/// </summary>
		public static TextWriter WriteItems<T>(this TextWriter writer, IEnumerable<T?> items, string delimiter, Action<TextWriter, T>? action)
			=> WriteItems(writer, items, delimiter, action, null, null);
		/// <summary>
		/// Simlar to the string.Join method, this method will write a collection of values with the specified delimiter.  It is more performant than string.Join.
		/// </summary>
		/// <returns>Current text writer</returns>
		public static TextWriter WriteItems<T>(this TextWriter writer, IEnumerable<T?> items, string delimiter, Action<TextWriter, T>? action = null, string? prefix = null, string? postfix = null) {
			int count = 0, total = items.Count();
			foreach (var item in items) {
				if (item != null) {
					if (prefix != null && count == 0) {
						writer.Append(prefix);
					}
					if (action == null) {
						writer.Append(item);
					} else {
						action.Invoke(writer, item);
					}
					count++;
					if (count != total) {
						writer.Append(delimiter);
					} else if (postfix != null) {
						writer.Append(postfix);
					}
				} else {
					total--;
				}
			}
			return writer;
		}
	}
}