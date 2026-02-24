using System.Collections.Generic;

namespace ctre_wp7
{
    internal static class LegacyApiExtensions
    {
        public static bool TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, ref TValue value)
        {
            if (dictionary.TryGetValue(key, out TValue existing))
            {
                value = existing;
                return true;
            }

            return false;
        }

        public static char get_Chars(this string value, int index)
        {
            return value[index];
        }
    }
}
