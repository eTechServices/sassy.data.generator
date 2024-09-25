using System;
using System.Collections.Generic;
using System.Linq;

namespace sassy.bulk.ExtensionsUtil
{
    internal static class StringExtension
    {
        public static IEnumerable<string> ToEnumerable(this string value) => string.IsNullOrEmpty(value) ? Enumerable.Empty<string>() : value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        
    }
}
