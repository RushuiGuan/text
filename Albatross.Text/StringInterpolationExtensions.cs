using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

namespace Albatross.Text {
	/// <summary>
	/// Provides string interpolation capabilities using ${expression} syntax, replacing expressions with evaluated values.
	/// </summary>
	public static class StringInterpolationExtensions {
		/// <summary>
		/// Replaces ${expression} placeholders in the input string using the provided evaluation function.
		/// </summary>
		/// <param name="input">The string containing ${expression} placeholders.</param>
		/// <param name="func">A function that receives the expression name and returns the replacement value.</param>
		/// <returns>The input string with all placeholders replaced.</returns>
		/// <exception cref="InvalidOperationException">Thrown when expression evaluation fails.</exception>
		public static string Interpolate(this string input, Func<string, string> func) {
			return Interpolate<object?>(input, (name, _) => func(name), null, true);
		}

		/// <summary>
		/// Regex pattern that matches ${expression} syntax where expression cannot start or end with whitespace.
		/// </summary>
		public const string ExpressionSearchPattern = @"\$\{(?!\s)(.+?)(?<![\s])}";

		/// <summary>
		/// Pre-compiled regex for efficient repeated expression matching.
		/// </summary>
		public static readonly Regex ExpressionSearchRegex = new Regex(ExpressionSearchPattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		/// <summary>
		/// Replaces ${expression} placeholders using a function that receives both the expression and a context value.
		/// </summary>
		/// <typeparam name="T">The type of the context value passed to the evaluation function.</typeparam>
		/// <param name="input">The string containing ${expression} placeholders.</param>
		/// <param name="func">A function that receives the expression name and context value, returning the replacement.</param>
		/// <param name="value">The context value passed to the evaluation function.</param>
		/// <param name="throwException">If true, throws on evaluation failure; if false, leaves the placeholder unchanged.</param>
		/// <returns>The input string with placeholders replaced.</returns>
		public static string Interpolate<T>(this string input, Func<string, T, string> func, T value, bool throwException) {
			return ExpressionSearchRegex.Replace(input, (match) => {
				string expression = match.Groups[1].Value;
				try {
					return func(expression, value);
				} catch (Exception err) {
					if (throwException) {
						throw new InvalidOperationException($"expression parsing exception: {expression}, {err.Message}");
					} else {
						return match.Groups[0].Value;
					}
				}
			});
		}

		[Obsolete("Use Interpolate(this string, Func<string, string>, bool) instead.  This method will be removed in the next release.  The dependency to Microsoft.Extensions.Logging.Abstractions will also be removed")]
		public static string Interpolate<T>(this string input, Func<string, T, string> func, T value, bool throwException = false, ILogger? logger = null) {
			return ExpressionSearchRegex.Replace(input, (match) => {
				string expression = match.Groups[1].Value;
				try {
					return func(expression, value);
				} catch (Exception err) {
					if (throwException) {
						throw new InvalidOperationException($"expression parsing exception: {expression}, {err.Message}");
					} else {
						logger?.LogError(err, "expression parsing exception: {expression}", expression);
						return match.Groups[0].Value;
					}
				}
			});
		}
	}
}