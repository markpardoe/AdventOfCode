using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.DataStructures
{

    /// <summary>
    /// Used to count the number of instances of a value that are added to the collection.
    /// Adding an item increments the number of items by 1.
    /// Removing decreases number of items by 1 - if total <=0 then the item is removed completely.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemCounter<T> :ICollection<T>
    {
        private readonly Dictionary<T, long> _internalDictionary = new Dictionary<T, long>();


        public bool Remove(T item)
        {
            if (_internalDictionary.ContainsKey(item))
            {
                var value = _internalDictionary[item] - 1;
                if (value <= 0)
                {
                    _internalDictionary.Remove(item);
                }
                else
                {
                    _internalDictionary[item] = value;
                }

                return true;
            }

            return false;
        }

        public void RemoveKey(T item)
        {
            if (_internalDictionary.ContainsKey(item))
            {
                _internalDictionary.Remove(item);
            }
        }

        public int Count => _internalDictionary.Count;
        public bool IsReadOnly => false;


        public void Clear()
        {
            _internalDictionary.Clear();
        }

        public bool Contains(T item)
        {
            return _internalDictionary.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internalDictionary.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            // item found
            if (_internalDictionary.ContainsKey(item))
            {
                _internalDictionary[item] = _internalDictionary[item] + 1;
            }
            else
            {
                _internalDictionary[item] = 1;
            }
        }

        public long this[T item]
        {
            get
            {
                if (!_internalDictionary.ContainsKey(item))
                {
                    return 0;
                }
                else
                {
                    return _internalDictionary[item];
                }
            }
        }

        public Dictionary<T, long>.KeyCollection Keys => _internalDictionary.Keys;
        public Dictionary<T, long>.ValueCollection Values => _internalDictionary.Values;


        public long Sum => _internalDictionary.Values.Sum();

    }
}
