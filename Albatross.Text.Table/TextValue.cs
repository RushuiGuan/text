namespace Albatross.Text.Table {
	public record struct TextValue {
		public TextValue(string text, int displayWidth) {
			Text = text;
			DisplayWidth = displayWidth;
		}
		public TextValue(string text) : this(text, text.Length) {
		}
		public string Text { get; }
		public int DisplayWidth { get; }
	}
}
