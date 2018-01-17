#pragma warning disable CS1591

using System.Collections;
using System.Collections.Generic;

// Taken from https://github.com/nhibernate/nhibernate-core/blob/master/src/NHibernate/Util/NullableDictionary.cs
namespace Sylveon.Memorize
{
    /// <summary>
    /// This is for internal use. You can use it if you want,
    /// but don't rely on its behavior or presence across
    /// versions.
    /// </summary>
    public class NullableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private TValue _nullValue;
        private bool _gotNullValue;
        private readonly Dictionary<TKey, TValue> _dict;

        public NullableDictionary()
        {
            _dict = new Dictionary<TKey, TValue>();
        }

        public NullableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dict = new Dictionary<TKey, TValue>(comparer);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                return _gotNullValue;
            }
            else
            {
                return _dict.ContainsKey(key);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                _nullValue = value;
                _gotNullValue = true;
            }
            else
            {
                _dict[key] = value;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                if (_gotNullValue)
                {
                    _nullValue = default(TValue);
                    _gotNullValue = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _dict.Remove(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                if (_gotNullValue)
                {
                    value = _nullValue;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
            else
            {
                return _dict.TryGetValue(key, out value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null && _gotNullValue)
                {
                    return _nullValue;
                }
                else if (key != null)
                {
                    return _dict[key];
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set
            {
                if (key == null)
                {
                    _nullValue = value;
                    _gotNullValue = true;
                }
                else
                {
                    _dict[key] = value;
                }
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                if (_gotNullValue)
                {
                    List<TKey> keys = new List<TKey>(_dict.Keys);
                    keys.Add(default);
                    return keys;
                }
                else
                {
                    return _dict.Keys;
                }
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                if (_gotNullValue)
                {
                    List<TValue> values = new List<TValue>(_dict.Values);
                    values.Add(_nullValue);
                    return values;
                }
                else
                {
                    return _dict.Values;
                }
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, TValue> kvp in _dict)
            {
                yield return kvp;
            }

            if (_gotNullValue)
            {
                yield return new KeyValuePair<TKey, TValue>(default, _nullValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
            {
                _nullValue = item.Value;
                _gotNullValue = true;
            }
            else
            {
                _dict.Add(item.Key, item.Value);
            }
        }

        public void Clear()
        {
            _dict.Clear();
            _nullValue = default;
            _gotNullValue = false;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (TryGetValue(item.Key, out TValue val))
            {
                return Equals(item.Value, val);
            }
            else
            {
                return false;
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get
            {
                if (_gotNullValue)
                {
                    return _dict.Count + 1;
                }
                else
                {
                    return _dict.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}