using System;

namespace Albatross.Text.Table {
	/// <summary>
	/// a data structure to hold a text value and its width.  There are situations where the display width of the text is not the
	/// same as the actual width of the text.  For example, if the text is a markdown link [Google](https://www.google.com), the 
	/// display width is only 6 , but the actual width is 30.  The truncate method is used to truncate the text to fit a certain width.
	/// The default truncate method is <see cref="Extensions.TruncateText(string, int)"/>.  It simply truncates the text to the display width.
	/// See <see cref="Extensions.TruncateMarkdownLink(string, int)"/> and <see cref="Extensions.TruncateSlackLink(string, int)"/> for examples of 
	/// trancating text such as a mark down link.
	/// </summary>
	public record struct TextValue {
		public TextValue(string text, int textWidth, Func<string, int, string> truncate) {
			Text = text;
			TextWidth = textWidth;
			this.Truncate = truncate;
		}
		public TextValue(string text)
			: this(text, text.Length, Extensions.TruncateText) { }

		public Func<string, int, string> Truncate { get; }
		public string Text { get; }
		public int TextWidth { get; }
		public string GetText(int displayWidth, bool alignRight) {
			if (displayWidth == TextWidth) {
				return this.Text;
			} else if (displayWidth < TextWidth) {
				return Truncate(this.Text, displayWidth);
			} else {
				var padding = " ".PadRight(displayWidth - TextWidth);
				if (alignRight) {
					return $"{padding}{this.Text}";
				} else {
					return $"{this.Text}{padding}";
				}
			}
		}
	}
}
