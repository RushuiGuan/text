using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Albatross.Text.Test {
	public class TestTextWriterExtensions {
		[Theory]
		[InlineData(",", "a,b,c,d", null, null, "a,b,c,d")]
		[InlineData(".", "a,b,c,d", "---", "***", "---a.b.c.d***")]
		[InlineData(".", "", "---", "***", "")]
		[InlineData(".", "a,b,,d", "---", "***", "---a.b.d***")]
		public void TestWriteItems(string delimiter, string data, string? prefix, string? postfix, string expected) {
			var array = data.Split(",", StringSplitOptions.None).Select(x => x == "" ? null : x).ToArray();
			var result = new StringWriter().WriteItems(array, delimiter, null, prefix, postfix).ToString();
			Assert.Equal(expected, result);

			result = new StringWriter().WriteItems(array, delimiter, (w, t) => w.Write(t), prefix, postfix).ToString();
			Assert.Equal(expected, result);
		}
	}
}