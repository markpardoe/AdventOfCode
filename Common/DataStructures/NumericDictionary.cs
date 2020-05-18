using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common.DataStructures
{
    /// <summary>
    /// Dictionary implementation that holds numeric values.
    /// Returns 0 as a default value if a key doesn't exist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NumericDictionary<T> : Dictionary<T, long>
    {
        public NumericDictionary() : base() { }


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
                    return 0;
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
