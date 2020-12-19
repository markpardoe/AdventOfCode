using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// Base class for a mapping implementation
    /// </summary>
    /// <typeparam name="TPosition"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class MapBase<TPosition, TValue> where TPosition:IEquatable<TPosition>
    {
        protected MapBase(TValue defaultValue)
        {
            DefaultValue = defaultValue;
        }

        protected readonly Dictionary<TPosition, TValue> Map = new Dictionary<TPosition, TValue>();
        protected readonly TValue DefaultValue;

        protected int DrawPadding { get; set; } = 0;   // Empty spaces to include when drawing the map

        public virtual void Add(TPosition key, TValue value) => AddOrReplace(key, value);
        public virtual TValue this[TPosition position]
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

        protected virtual void AddOrReplace(TPosition key, TValue value)
        {
            if (Map.ContainsKey(key))
            {
                Map[key] = value;
            }
            else
            {
                Map.Add(key, value);
            }
        }

        protected abstract IEnumerable<TPosition> GetAvailableNeighbors(TPosition position);
        public abstract string DrawMap();
        public abstract IEnumerable<KeyValuePair<TPosition, TValue>> GetBoundedEnumerator(int padding = 0);

       
    }
}