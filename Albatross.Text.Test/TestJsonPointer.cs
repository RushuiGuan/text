using Albatross.Text.CliFormat;
using System;
using Xunit;

namespace Albatross.Text.Test {
	public class TestJsonPointer {
		[Theory]
		[InlineData("/1a", "/1")]
		[InlineData("/a*", "/a")]
		[InlineData("/", "/")]
		[InlineData("//", "//")]
		[InlineData("/a", "/a")]
		[InlineData("/1/2", "/1/2")]
		[InlineData("/a/b", "/a/b")]
		public void TestParse(string text, string expected) {
			 var factory = new JsonPointerLiteralFactory();
			 var expr = factory.Parse(text, 0, out var next);
			 Assert.NotNull(expr);
			 var value = expr.Eval(x => new Object());
			 Assert.Equal(expected, value?.ToString());
		}
		
		[Theory]
		[InlineData("")]
		[InlineData("a/b")]
		public void TestParseFailure(string text) {
			var match = JsonPointerLiteralFactory.Regex.Match(text);
			Assert.False(match.Success);
		}
	}
}