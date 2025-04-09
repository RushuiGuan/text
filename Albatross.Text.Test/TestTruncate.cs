using Albatross.Text.Table;
using Xunit;

namespace Albatross.Text.Test {
	public class TestTruncate {
		[Theory]
		[InlineData("", 1, "")]
		[InlineData("a", 1, "a")]
		[InlineData("aa", 1, "a")]
		[InlineData("aa", 4, "aa")]
		public void TestTruncateText(string text, int displayWidth, string expected) {
			var result = text.TruncateText(displayWidth);
			Assert.Equal(expected, result);
		}

		[Fact]
		public void TestTruncateMarkdownLink() {
			var text = "[Google](https://www.google.com)";
			var result = text.TruncateMarkdownLink(5);
			Assert.Equal("[Googl](https://www.google.com)", result);
		}

		[Fact]
		public void TestTruncateSlackLink() {
			var text = "<https://www.google.com|Google>";
			var result = text.TruncateSlackLink(5);
			Assert.Equal("<https://www.google.com|Googl>", result);
		}
	}
}
