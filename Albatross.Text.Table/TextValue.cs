using System;

namespace Albatross.Text.Table {
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
