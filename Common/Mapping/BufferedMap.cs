using System;
using System.Collections.Generic;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// Mapbase with a buffer.
    /// allowing 'next' values to be stored and the entire map updated in one operation
    /// </summary>
    /// <typeparam name="TPosition"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class BufferedMap<TPosition, TValue> : MapBase<TPosition, TValue> where TPosition : IEquatable<TPosition>
    {
        private readonly Dictionary<TPosition, TValue> _buffer = new Dictionary<TPosition, TValue>();

        protected BufferedMap(TValue defaultValue) : base(defaultValue) {}

        public void AddToBuffer(TPosition key, TValue value)
        {
            if (_buffer.ContainsKey(key))
            {
                _buffer[key] = value;
            }
            else
            {
                _buffer.Add(key, value);
            }
        }

        // Clears current map and replaces it with values from the buffer.
        // Wipes the buffer once copy is complete.
        public void UpdateFromBuffer()
        {
            Map.Clear();
            foreach (var (key, value) in _buffer)
            {
                Map.Add(key, value);
            }
            _buffer.Clear();
        }
    }
}