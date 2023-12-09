using System.Collections;

namespace HyPlayer.NeteaseApi.Bases;

public abstract class RawApiActualRequestBase : ActualRequestBase, IDictionary<string, string>
{

    private readonly Dictionary<string, string> _backingDictionary = new();
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _backingDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_backingDictionary).GetEnumerator();
    }

    public void Add(KeyValuePair<string, string> item)
    {
        _backingDictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _backingDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _backingDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        ((ICollection)_backingDictionary).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _backingDictionary.Remove(item.Key);
    }

    public int Count => _backingDictionary.Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string,string>>)_backingDictionary).IsReadOnly;

    public void Add(string key, string value)
    {
        _backingDictionary.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _backingDictionary.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _backingDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return _backingDictionary.TryGetValue(key, out value);
    }

    public string this[string key]
    {
        get => _backingDictionary[key];
        set => _backingDictionary[key] = value;
    }

    public ICollection<string> Keys => ((IDictionary<string,string>)_backingDictionary).Keys;

    public ICollection<string> Values => ((IDictionary<string,string>)_backingDictionary).Values;
}