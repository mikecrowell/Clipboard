using System.Collections.Specialized;
using System.Linq;

namespace FieldTool.ClipboardLookup
{
    public static class CollectionExtensions
    {
        public static bool ContainsExpectedValue(this NameValueCollection collection, string key, string expectedValue)
        {
            return collection.AllKeys.Contains(key) && collection[key] == expectedValue;
        }
    }
}