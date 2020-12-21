using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// Base class for a mapping implementation
    /// </summary>
    /// <typeparam name="TPosition"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class MapBase<TPosition, TValue> : IEnumerable<KeyValuePair<TPosition, TValue>> where TPosition : IEquatable<TPosition>
    {
        protected MapBase(TValue defaultValue)
        {
            DefaultValue = defaultValue;
        }

        protected readonly Dictionary<TPosition, TValue> Map = new Dictionary<TPosition, TValue>();
        protected readonly TValue DefaultValue;

        public void Add(TPosition key, TValue value) => AddOrReplace(key, value);
        public TValue this[TPosition position]
        {
            get
            {
                if (!Map.ContainsKey(position))
                {
                    return DefaultValue;
                }
                else
                {
                    return Map[position];
                }
            }
            set => AddOrReplace(position, value);
        }
        public int CountValue(TValue item) => Map.Values.Count(v => v.Equals(item));

        private void AddOrReplace(TPosition key, TValue value)
        {
            if (Map.ContainsKey(key))
            {
                Map[key] = value;
            }
            else
            {
                Map.Add(key, value);
            }

            OnMapUpdated(key, value);
        }
        protected virtual void OnMapUpdated(TPosition key, TValue value) { }
        protected virtual void BeforeMapUpdated(TPosition key, TValue value) { }

        protected abstract IEnumerable<TPosition> GetAvailableNeighbors(TPosition position);
        public abstract string DrawMap(int padding = 0);
        public abstract IEnumerable<KeyValuePair<TPosition, TValue>> GetBoundedEnumerator(int padding = 0);

        public IEnumerator<KeyValuePair<TPosition, TValue>> GetEnumerator()
        {
            return Map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}