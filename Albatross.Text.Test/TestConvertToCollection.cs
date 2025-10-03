using System;
using System.Collections.Generic;
using Xunit;

namespace Albatross.Text.Test {
	public class TestConvertToCollection {
		[Fact]
		public void TestCreation() {
			var type = typeof(List<string>);
			var data = new string[] { "a", "b", "c" };
			var collection = Activator.CreateInstance(type, [data]);
			Assert.NotNull(collection);
		}
	}
}