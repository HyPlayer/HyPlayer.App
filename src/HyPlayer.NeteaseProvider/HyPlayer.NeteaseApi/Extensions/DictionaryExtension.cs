namespace HyPlayer.NeteaseApi.Extensions;

public static class DictionaryExtension
{
    public static TValue? GetValueOrDefault<TKey,TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
    {
        return dictionary.TryGetValue(key, out TValue? value) ? value : defaultValue;
    }
}