using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Albatross.Text.Table {
	public static class Extensions {
		public static TableOptions<T> Register<T>(this TableOptionFactory factory, TableOptions<T> options) {
			factory.Register(options);
			return options;
		}
		public static void MarkdownTable<T>(this IEnumerable<T> items, TextWriter writer, TableOptions<T>? options = null) {
			options = options ?? TableOptionFactory.Instance.Get<T>();
			writer.WriteItems(options.Headers, "|").WriteLine();
			writer.WriteItems(options.Build().Select(x => "-").ToArray(), "|").WriteLine();
			foreach (var item in items) {
				writer.WriteItems(options.GetValue(item).Select(x => x.Text), "|").WriteLine();
			}
		}

		static Regex MarkdownLinkRegex = new Regex(@"\[(?<text>[^\]]+)\]\((?<url>[^)]+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
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
		public static string TruncateText(this string text, int displayWidth) {
			if (displayWidth == 0) { return string.Empty; }
			if (text.Length > displayWidth) {
				return text.Substring(0, displayWidth);
			} else {
				return text;
			}
		}
	}
}
