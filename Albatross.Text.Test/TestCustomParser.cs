using Albatross.Expression;
using Albatross.Expression.Nodes;
using Albatross.Text.CliFormat;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Albatross.Text.Test {
	public class TestCustomParser {
		[Theory]
		[InlineData("json()", "json")]
		[InlineData("json(1, 2, 3)", "json", "1", "2", "3")]
		[InlineData("json(/1/a)", "json", "/1/a")]
		[InlineData("json(/a/b, /c/d)", "json", "/a/b", "/c/d")]
		public void Run(string text, string name, params string[] expectedParameters) {
			var parser  = Albatross.Text.CliFormat.Extensions.BuildCustomParser();
			var expr = Expression.Parsing.Extensions.Build(parser, text);
			var prefix = expr as IPrefixExpression;
			Assert.NotNull(prefix);
			Assert.Equivalent(name, prefix.Name);
			var parameters = prefix.Operands.Select(x=>x.Eval(_=>new object()).ConvertToString()).ToArray();
			Assert.Equivalent(expectedParameters, parameters);
		}
	}
}