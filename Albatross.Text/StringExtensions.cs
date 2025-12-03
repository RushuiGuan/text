using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Albatross.Text {
	public static class StringExtensions {
		/// <summary>
		/// Return the text with the first character in upper case.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(text))]
		public static string? ProperCase(this string? text, CultureInfo? culture = null) {
			if (!string.IsNullOrEmpty(text)) {
				string result = text!.Substring(0, 1).ToUpper(culture ?? CultureInfo.CurrentCulture);
				if (text.Length > 1) {
					result = result + text.Substring(1);
				}
				return result;
			} else {
				return text;
			}
		}


		/// <summary>
		/// Return the camel case version of the text.
		/// The method will convert the leading upper case characters to lower case until it reaches a lower case character or the end of the string.
		/// 
		/// Here are some examples:
		/// CUSIP => cusip
		/// BBYellow => bbYellow
		/// Test => test
		/// test => test
		/// </summary>
		/// <param name="text"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(text))]
		public static string? CamelCase(this string? text, CultureInfo? culture = null) {
			if (!string.IsNullOrEmpty(text)) {
				if (char.IsLower(text![0])) {
					return text;
				} else {
					int marker = 0;
					var sb = new StringBuilder(text);
					for (int i = 0; i < sb.Length; i++) {
						char c = sb[i];
						if (char.IsUpper(c)) {
							if (i == 0 || marker == i && (i == sb.Length - 1 || char.IsUpper(sb[i + 1]))) {
								sb[i] = char.ToLower(c, culture ?? CultureInfo.CurrentCulture);
								marker++;
							}
						}
					}
					return sb.ToString();
				}
			} else {
				return text;
			}
		}

		/// <summary>
		/// match a string against a glob pattern.  ? matches any single character and * matches any characters
		/// If dealing with file systems directly, please use `Microsoft.Extensions.FileSystemGlobbing` instead.
		/// The method will return false for null or empty text string.  It will throw an ArgumentException if the 
		/// parameter <paramref name="globPattern"/> is null.
		/// </summary>
		/// <param name="text">The string to be tested</param>
		/// <param name="globPattern">A glob pattern where ? matches any single character and * matches any characters</param>
		/// <returns></returns>
		public static bool Like(this string? text, string globPattern) {
			if (string.IsNullOrEmpty(text)) { return false; }
			if (globPattern == null) { throw new ArgumentException($"{nameof(globPattern)} cannot be null"); }
			string pattern = $"^{Regex.Escape(globPattern).Replace(@"\*", ".*").Replace(@"\?", ".")}$";a
			var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			return regex.IsMatch(text);
		}

		/// <summary>
		/// Postfix the specified character to the text if the text is not empty and does not end with the said character
		/// </summary>
		/// <param name="text"></param>
		/// <param name="character"></param>
		/// <returns></returns>
		[return: NotNullIfNotNull(nameof(text))]
		public static string? PostfixIfNotNullOrEmpty(this string? text, char character) {
			if (!string.IsNullOrEmpty(text) && !text!.EndsWith($"{character}")) {
				return text + character;
			} else {
				return text;
			}
		}

		/// <summary>
		/// Replace multiple characters in a string with a single character
		/// </summary>
		/// <param name="text"></param>
		/// <param name="replacementCharacter"></param>
		/// <param name="targetCharacters"></param>
		/// <returns></returns>
		public static string ReplaceMultipleChars(this string text, char replacementCharacter, params char[] targetCharacters) {
			var array = new char[text.Length];
			IEnumerable<char> set = targetCharacters.Length < 10 ? targetCharacters : new HashSet<char>(targetCharacters);
			for (int i = 0; i < text.Length; i++) {
				if (set.Contains(text[i])) {
					array[i] = replacementCharacter;
				} else {
					array[i] = text[i];
				}
			}
			return new string(array);
		}

		/// <summary>
		/// Remove the specified value from the start of the line if it exists.
		/// </summary>
		/// <param name="line"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string TrimStart(this string line, string value) {
			if (line.StartsWith(value)) {
				return line.Substring(value.Length);
			} else {
				return line;
			}
		}

		/// <summary>
		/// Remove the specified value from the end of the line if it exists.
		/// </summary>
		/// <param name="line"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string TrimEnd(this string line, string value) {
			if (line.EndsWith(value)) {
				return line.Substring(0, line.Length - value.Length);
			} else {
				return line;
			}
		}

		/// <summary>
		/// Try to get the text from the line with the given delimiter.  The method can be called repeatedly on the same line
		/// until the end of the line is reached.  This method is faster than using String.Split.  Good for parsing delimited text.
		/// But does not handle escaped delimiters.
		/// </summary>
		/// <param name="line"></param>
		/// <param name="delimiter"></param>
		/// <param name="offset"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool TryGetText(this string line, char delimiter, ref int offset, [NotNullWhen(true)] out string? text) {
			if (offset == line.Length + 1) {
				text = null;
				return false;
			} else {
				var index = line.IndexOf(delimiter, offset);
				if (index == -1) {
					text = line.Substring(offset);
					offset = line.Length + 1;
					return true;
				} else {
					text = line.Substring(offset, index - offset);
					offset = index + 1;
					return true;
				}
			}
		}

		/// <summary>
		/// write a decimal number and remove its trailing zeros after the decimal point
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Decimal2CompactText(this decimal value) => $"{value:G29}";

		/// <summary>
		/// this method trims the trailing zeros of a decimal number in string format.  Since
		/// the input is a string, it could contain thousands separator and the decimal point could be culture specific.
		/// Here is an example to format 2000.5640000 as 2,000.564.
		/// var d = 2000.56400000M;
		/// var text = d.ToString("#,0.#############################").Decimal2CompactText()
		/// </summary>
		/// <param name="numberText"></param>
		/// <param name="cultureInfo"></param>
		/// <returns></returns>
		public static string Decimal2CompactText(this string numberText, CultureInfo? cultureInfo = null) {
			var culture = cultureInfo ?? CultureInfo.CurrentCulture;
			char decimalPoint = culture.NumberFormat.NumberDecimalSeparator[0];
			int lastDigitToTrim = numberText.Length;
			for (int i = numberText.Length - 1; i >= 0; i--) {
				var c = numberText[i];
				if ((c == '0' || c == decimalPoint) && lastDigitToTrim == i + 1) {
					lastDigitToTrim = i;
				}
				if (c == decimalPoint) {
					if (lastDigitToTrim != numberText.Length) {
						if (lastDigitToTrim == 0) {
							return "0";
						} else {
							return numberText.Substring(0, lastDigitToTrim);
						}
					} else {
						return numberText;
					}
				}
			}
			return numberText;
		}

		public static string MarkdownLink(string text, string url) {
			return $"[{text}]({url})";
		}

		public static string SlackLink(string text, string url) {
			return $"<{url}|{text}>";
		}
	}
}