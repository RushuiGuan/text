using Albatross.Expression;
using Albatross.Expression.Prefix;
using Array = System.Array;

namespace Albatross.Text.CliFormat.Operations {
    /// <summary>
    /// Return a subset of a collection starting at the specified index.  The second required parameter is the starting index.  The third optional parameter is the number of elements to return.
    /// If the third parameter is not specified, all elements from the starting index to the end of the collection will be returned.
    /// This operation behaves similarly to the Substring operation on strings.  But it doesn't return errors, it will return as many elements as possible.
    /// If the starting index is out of range, an empty collection will be returned.
    /// </summary>
    public class Subset : PrefixExpression {
        public Subset() : base("subset", 2, 3) { }

        protected override object Run(List<object> operands) {
            var value = operands[0].ConvertToCollection(out var elementType);
            int start = operands[1].ConvertToInt();
            if (start < 0) { start = 0; }
            int count = value.Count - start;
            if (count < 0) { count = 0; }
            if (operands.Count > 2) {
                count = Math.Min(count, operands[2].ConvertToInt());
            }
            var array = Array.CreateInstance(elementType, count);
            for (int i = 0; i < count; i++) {
                array.SetValue(value[i + start], i);
            }
            return array;
        }
    }
}