using Albatross.Reflection;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	/// <summary>
	/// Provides extension methods for converting objects to collections for formatting operations.
	/// </summary>
	public static class Extensions {
		/// <summary>
		/// Converts an object to an enumerable collection, extracting the element type information.
		/// If the input is already a collection, returns it as-is. If it's a single object, wraps it in a single-element array.
		/// </summary>
		/// <param name="input">The object to convert to a collection.</param>
		/// <param name="type">Outputs the element type of the collection. For single objects, this is the object's type. For collections, this is the element type.</param>
		/// <returns>An enumerable collection containing the input object(s).</returns>
		/// <remarks>
		/// This method is essential for operations that need to handle both single objects and collections uniformly.
		/// The type parameter provides the necessary type information for downstream formatting operations.
		/// </remarks>
		public static IEnumerable<object> ConvertToCollection(this object input, out Type type) {
			type = input.GetType();
			if (type.TryGetCollectionElementType(out var elementType)) {
				type = elementType;
				return ((IEnumerable)input).Cast<object>();
			} else {
				return new [] { input };
			}
		}
	}
}