using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.DataStructures
{
    /// <summary>
    /// Dictionary implementation that holds numeric values.
    /// Returns 0 as a default value if a key doesn't exist.
    /// Creates new or updates existing entry
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NumericDictionary<T> : Dictionary<T, long>
    {
        public NumericDictionary() : base() { }
        public int DefaultValue { get; } = 0;

        public NumericDictionary(int defaultValue = 0) :base()
        {
            DefaultValue = defaultValue;
        }

        public new void Add(T key, long value)
        {
            if (!base.ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                base[key] += value;
            }
        }

        public new long this[T key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    return DefaultValue;
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                if (!base.ContainsKey(key))
                {
                    base.Add(key, value);
                }
                else
                {
                    base[key] = value;
                }
            }
        }
    }
}
