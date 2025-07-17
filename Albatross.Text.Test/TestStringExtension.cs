using Albatross.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Albatross.Text.Test {
	public class TestStringExtension {
		[Theory]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData("a", "A")]
		[InlineData("A", "A")]
		[InlineData("abc", "Abc")]
		[InlineData("ABC", "ABC")]
		[InlineData("aBC", "ABC")]
		public void TestProperCase(string? text, string? expected) {
			var result = text.ProperCase();
			Assert.Equal(expected, result);
		}
		
		[Theory]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData("a", "a")]
		[InlineData("A", "a")]
		[InlineData("abc", "abc")]
		[InlineData("ABC", "abc")]
		[InlineData("aBC", "aBC")]
		[InlineData("CUSIP", "cusip")]
		[InlineData("BBYellow", "bbYellow")]
		public void TestCamelCase(string? text, string? expected) {
			var result = text.CamelCase();
			Assert.Equal(expected, result);
		}
		
		[Theory]
		[InlineData("test", "*", true)]
		[InlineData("test", "t*", true)]
		[InlineData("test", "*t", true)]
		[InlineData("test", "t???", true)]
		[InlineData("test", "t??", false)]
		[InlineData("test", "a*", false)]
		[InlineData("", "*", false)]
		[InlineData(null, "*", false)]
		public void TestLike(string? input, string pattern, bool expected) {
			var result = input.Like(pattern);
			Assert.Equal(expected, result);
		}
		
		[Theory]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData(".", ".")]
		[InlineData("AB", "AB.")]
		[InlineData("AB.", "AB.")]
		public void TestPostfixIfNotNullOrEmpty(string? text, string? expected) {
			var result = text.PostfixIfNotNullOrEmpty('.');
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("abcd", "abcd", ' ', 'x')]
		[InlineData(" bcd", "abcd", ' ', 'a')]
		public void TestReplaceMultiCharacters(string expected, string input, char replacedWith, params char[] targets) {
			var result = input.ReplaceMultipleChars(replacedWith, targets);
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("", "a", "")]
		[InlineData("", "", "")]
		[InlineData("a", "a", "")]
		[InlineData("ab", "a", "b")]
		[InlineData("ab", "", "ab")]
		[InlineData("abc", "ab", "c")]
		[InlineData("abc", "abc", "")]
		public void TestTrimStart(string text, string trim, string expected) {
			var result = text.TrimStart(trim);
			Assert.Equal(expected, result);
		}
		[Theory]
		[InlineData("", "a", "")]
		[InlineData("", "", "")]
		[InlineData("a", "a", "")]
		[InlineData("ab", "b", "a")]
		[InlineData("ab", "", "ab")]
		[InlineData("abc", "bc", "a")]
		[InlineData("abc", "abc", "")]
		public void TestTrimEnd(string text, string trim, string expected) {
			var result = text.TrimEnd(trim);
			Assert.Equal(expected, result);
		}

		
		[Theory]
		[InlineData("a  b c d", ' ', "a..b.c.d")]
		[InlineData("a b c d", ' ', "a.b.c.d")]
		[InlineData("abcd", ' ', "abcd")]
		public void TestTryGetText(string input, char delimiter, string expected) {
			var list = new List<string>();
			int offset = 0;
			while (input.TryGetText(delimiter, ref offset, out var text)) {
				list.Add(text);
			}
			var result = string.Join('.', list);
			Assert.Equal(expected, result);
		}
	
		[Theory]
		[InlineData("0", "0")]
		[InlineData("0.0", "0")]
		[InlineData("0.1", "0.1")]
		[InlineData("0.10", "0.1")]
		[InlineData("0.100", "0.1")]
		[InlineData("0.123", "0.123")]
		[InlineData(".0", "0")]
		[InlineData("0.", "0")]
		[InlineData("1.0", "1")]
		[InlineData("1", "1")]
		[InlineData("10.0101010", "10.010101")]
		[InlineData("10.123", "10.123")]
		[InlineData("10.1230", "10.123")]
		[InlineData("10.1230000", "10.123")]
		[InlineData("10", "10")]
		[InlineData("123", "123")]
		[InlineData("1000000", "1000000")]
		public void TestTrimDecimal(string number, string expected) {
			var value = decimal.Parse(number);
			var result = value.Decimal2CompactText();
			Assert.Equal(expected, result);
		}
		
		[Theory]
		[InlineData("0", "0", "en-US")]
		[InlineData("0.0", "0", "en-US")]
		[InlineData("0.1", "0.1", "en-US")]
		[InlineData("0.10", "0.1", "en-US")]
		[InlineData("0.100", "0.1", "en-US")]
		[InlineData("0.123", "0.123", "en-US")]
		[InlineData(".0", "0", "en-US")]
		[InlineData("0.", "0", "en-US")]
		[InlineData("1.0", "1", "en-US")]
		[InlineData("1", "1", "en-US")]
		[InlineData("10.0101010", "10.010101", "en-US")]
		[InlineData("10.123", "10.123", "en-US")]
		[InlineData("10.1230", "10.123", "en-US")]
		[InlineData("10.1230000", "10.123", "en-US")]
		[InlineData("10", "10", "en-US")]
		[InlineData("123", "123", "en-US")]
		[InlineData("1000000", "1000000", "en-US")]
		[InlineData("1,000,000", "1,000,000", "en-US")]
		
		[InlineData("0", "0", "fr-FR")]
		[InlineData("0,0", "0", "fr-FR")]
		[InlineData("0,1", "0,1", "fr-FR")]
		[InlineData("0,10", "0,1", "fr-FR")]
		[InlineData("0,100", "0,1", "fr-FR")]
		[InlineData("0,123", "0,123", "fr-FR")]
		[InlineData(",0", "0", "fr-FR")]
		[InlineData("0,", "0", "fr-FR")]
		[InlineData("1,0", "1", "fr-FR")]
		[InlineData("1", "1", "fr-FR")]
		[InlineData("10,0101010", "10,010101", "fr-FR")]
		[InlineData("10,123", "10,123", "fr-FR")]
		[InlineData("10,1230", "10,123", "fr-FR")]
		[InlineData("10,1230000", "10,123", "fr-FR")]
		[InlineData("10", "10", "fr-FR")]
		[InlineData("123", "123", "fr-FR")]
		[InlineData("1000000", "1000000", "fr-FR")]
		public void TestTrimDecimalText(string number, string expected, string culture) {
			var result = number.Decimal2CompactText(CultureInfo.GetCultureInfo(culture));
			Assert.Equal(expected, result);
		}
	}
}