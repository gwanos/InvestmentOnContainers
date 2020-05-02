using System;
using System.Collections.Generic;

namespace Common.Extension
{
    public static class DictionaryExtension
    {
        public static Dictionary<TValue, TKey> ReverseKeyValue<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            var result = new Dictionary<TValue, TKey>();
            foreach (var kv in source)
            {
                result.Add(kv.Value, kv.Key);
                //if (!result.ContainsKey(entry.Value))
                //{
                    
                //}
            }
            return result;
        }
    }
}
