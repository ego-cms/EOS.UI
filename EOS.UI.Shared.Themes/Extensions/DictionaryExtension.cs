using System;
using System.Collections.Generic;
using System.Linq;

namespace EOS.UI.Shared.Themes.Extensions
{
    public static class DictionaryExtension
    {
        public static Dictionary<TKey, TValue> AddDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            
            return dict.Prepend(new KeyValuePair<TKey, TValue>(key, value)).ToDictionary(s => s.Key, s => s.Value);
        }
    }
}
