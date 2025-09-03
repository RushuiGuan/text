using Albatross.Text.CliFormat;
using System.Collections.Generic;
using Xunit;

namespace Albatross.Text.Test {
	public class TestCliFormat {
		public class Sample {
			public int Id { get; set; }
			public required string Name { get; set; }
		}
		
		//[Theory]
		//[InlineData("jsonarray()", Format.JsonArray, new string[] { })]
		//[InlineData("jsonscalar", Format.JsonScalar, new string[] { })]
		//public void VerifyFormatOption(string text, Format expectedFormat, IEnumerable<string> expectedParameters) {
		//	var options = text.GetFormatOptions();
		//	Assert.Equal(expectedFormat, options.Format);
		//	Assert.Equivalent(expectedParameters, options.Parameters);
		//}
	}
}