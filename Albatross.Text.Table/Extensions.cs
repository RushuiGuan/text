using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Albatross.Text.Table {
	/// <summary>
	/// Utility extension methods for table rendering, link truncation, and type detection.
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Renders a collection as a Markdown-formatted table.
		/// </summary>
		public static void MarkdownTable<T>(this IEnumerable<T> items, TextWriter writer, TableOptions<T>? options = null) {
			options = options ?? TableOptionFactory.Instance.Get<T>();
			writer.WriteItems(options.Headers, "|").WriteLine();
			writer.WriteItems(options.Build().Select(x => "-").ToArray(), "|").WriteLine();
			foreach (var item in items) {
				writer.WriteItems(options.GetValue(item).Select(x => x.Text), "|").WriteLine();
			}
		}

		static Regex MarkdownLinkRegex = new Regex(@"\[(?<text>[^\]]+)\]\((?<url>[^)]+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

		/// <summary>
		/// Truncates a Markdown link's display text while preserving the URL.
		/// </summary>
		public static string TruncateMarkdownLink(this string markdownLink, int displayWidth) {
			if (displayWidth == 0) { return string.Empty; }
			var match = MarkdownLinkRegex.Match(markdownLink);
			if (match.Success) {
				var text = match.Groups["text"].Value;
				var url = match.Groups["url"].Value;
				if (text.Length > displayWidth) {
					return $"[{text.Substring(0, displayWidth)}]({url})";
				} else {
					return markdownLink;
				}
			} else {
				return markdownLink.Substring(0, displayWidth);
			}
		}

		static Regex SlackLinkRegex = new Regex(@"<(?<url>[^|]+)\|(?<text>[^\>]+)>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

		/// <summary>
		/// Truncates a Slack link's display text while preserving the URL.
		/// </summary>
		public static string TruncateSlackLink(this string slackLink, int displayWidth) {
			if (displayWidth == 0) { return string.Empty; }
			var match = SlackLinkRegex.Match(slackLink);
			if (match.Success) {
				var text = match.Groups["text"].Value;
				var url = match.Groups["url"].Value;
				if (text.Length > displayWidth) {
					return $"<{url}|{text.Substring(0, displayWidth)}>";
				} else {
					return slackLink;
				}
			} else {
				return slackLink.Substring(0, displayWidth);
			}
		}

		/// <summary>
		/// Truncates text to the specified display width.
		/// </summary>
		public static string TruncateText(this string text, int displayWidth) {
			if (displayWidth == 0) { return string.Empty; }
			if (text.Length > displayWidth) {
				return text.Substring(0, displayWidth);
			} else {
				return text;
			}
		}

		/// <summary>
		/// Determines whether a type is a simple value type (primitives, string, DateTime, Guid, etc.).
		/// </summary>
		public static bool IsSimpleValue(this Type type) =>
			type == typeof(string) || type.IsPrimitive
			                       || type == typeof(DateTime)
			                       || type == typeof(DateOnly)
			                       || type == typeof(TimeOnly)
			                       || type == typeof(DateTimeOffset)
			                       || type == typeof(Guid);
	}
}