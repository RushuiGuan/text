using Albatross.Text.Table;
using Xunit;

namespace Albatross.Text.Test {
	public class TestTextValue {
		[Theory]
		[InlineData("a", 1, false, "a")]
		[InlineData("a", 2, false, "a ")]
		[InlineData("a", 2, true, " a")]
		public void TestTextValueText(string text, int displayWidth, bool alignRight, string expected) {
			var value = new TextValue(text);
			var result = value.GetText(displayWidth, alignRight);
			Assert.Equal(expected, result);
		}
	}
}
