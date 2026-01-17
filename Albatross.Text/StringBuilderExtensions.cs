using System.Text;

namespace Albatross.Text {
	/// <summary>
	/// Extension methods for <see cref="StringBuilder"/> that provide efficient string operations without creating intermediate strings.
	/// </summary>
	public static class StringBuilderExtensions {
		/// <summary>
		/// Determines whether the StringBuilder content ends with the specified string.
		/// </summary>
		/// <param name="sb">The StringBuilder to check.</param>
		/// <param name="text">The string to compare against the end of the StringBuilder.</param>
		/// <returns>True if the StringBuilder ends with the specified string; otherwise, false.</returns>
		public static bool EndsWith(this StringBuilder sb, string text) {
			if (sb.Length >= text.Length) {
				for (int i = 0; i < text.Length; i++) {
					if (sb[sb.Length - text.Length + i] != text[i]) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Determines whether the StringBuilder content ends with the specified character.
		/// </summary>
		/// <param name="sb">The StringBuilder to check.</param>
		/// <param name="c">The character to compare against the last character.</param>
		/// <returns>True if the StringBuilder ends with the specified character; otherwise, false.</returns>
		public static bool EndsWith(this StringBuilder sb, char c) {
			if (sb.Length >= 1) {
				return sb[sb.Length -1 ] == c;
			}
			return false;
		}
	}
}