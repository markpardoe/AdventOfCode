using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace AoC.Common.DataStructures
{
    /// <summary>
    /// List implementation that allows roll-over of values.
    /// Eg. List<string> strList = {"A", "B"}
    /// strList[0] = "A"
    /// strList[1] = "B"
    /// strList[2] = "A"
    /// strList[3] = "B"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepeatingList<T> : IList<T>
    {
        private readonly List<T> _data = new List<T>();

        private int ConvertIndex(int ix)
        {
            if (_data.Count == 0)
            {
                return 0;
            }
            else if (ix >= 0 && ix < _data.Count)
            {
                return ix;
            }
            else if (ix >= 0)
            {
                return ix % _data.Count;
            }
            else if (ix < 0)
            {
                return (ix - _data.Count) % _data.Count;
            }
            else throw new InvalidOperationException();
        }

        public RepeatingList(IEnumerable<T> range)
        {
            _data = new List<T>(range);
        }

        public RepeatingList() { }

        public T this[int index]
        {
            get => _data[ConvertIndex(index)];

            set => _data[ConvertIndex(index)] = value;
        }

        public int Count => _data.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(T item)
        {
            return _data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {

            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _data.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _data.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _data.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(ConvertIndex(index));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
