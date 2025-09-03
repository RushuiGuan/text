using Albatross.Reflection;
using System.Collections;

namespace Albatross.Text.CliFormat.Operations {
	public static class Extensions {
		public static IEnumerable ConvertToCollection(this object input, out Type type) {
			type = input.GetType();
			if (type.GetCollectionElementType(out var elementType)) {
				type = elementType;
				return (IEnumerable)input;
			} else {
				return new [] { input };
			}
		}
	}
}