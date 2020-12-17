using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC.AoC2020.Problems.Day17
{
    public class Map4d <TValue> 
    {
        protected readonly TValue _default;
        protected Dictionary<Position4d, TValue> _map = new Dictionary<Position4d, TValue>();
        
        public int MaxX => _map.Keys.Max(p => p.X);
        public int MinX => _map.Keys.Min(p => p.X);

        public int MaxY => _map.Keys.Max(p => p.Y);
        public int MinY => _map.Keys.Min(p => p.Y);

        public int MaxZ => _map.Keys.Max(p => p.Z);
        public int MinZ => _map.Keys.Min(p => p.Z);

        public int MaxW => _map.Keys.Max(p => p.W);
        public int MinW => _map.Keys.Min(p => p.W);

        public Map4d(TValue defaultValue)
        {
            _default = defaultValue;
            MapConverter = new Func<TValue, char?>(DefaultStringFunction);
        }

        public void Add(Position4d key, TValue value)
        {
            if (_map.ContainsKey(key))
            {
                _map[key] = value;
            }
            else
            {
                _map.Add(key, value);
            }
        }

        public void Add(int x, int y, int z, int w, TValue value) => Add(new Position4d(x, y, z, w), value);

        public  TValue this[Position4d position]
        {
            get
            {
                if (!_map.ContainsKey(position))
                {
                    return _default;
                }
                else
                {
                    return _map[position];
                }
            }
            set
            {
                if (!_map.ContainsKey(position))
                {
                    _map.Add(position, value);
                }
                else
                {
                    _map[position] = value;
                }
            }
        }

        public TValue this[int x, int y, int z, int w]
        {
            get => this[new Position4d(x, y, z, w)];
            set => this[new Position4d(x, y, z, w)] = value;
        }

        public int CountValue(TValue item) => _map.Values.Count(v => v.Equals(item));

        protected Func<TValue, char?> MapConverter;  // function to use for converting the value for mapping purposes
        private char? DefaultStringFunction(TValue value) => value?.ToString()[0];

       

        // Returns all positions within the region of the map (between min and max bounds)
        // Will return positions within bounds that are not keys in the map collection
        public IEnumerable<KeyValuePair<Position4d, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            int minZ = MinZ - padding;
            int maxZ = MaxZ + padding;
            int minY = MinY - padding;
            int maxY = MaxY + padding;
            int minX = MinX - padding;
            int maxX = MaxX + padding;

            int minW = MinW - padding;
            int maxW = MaxW + padding;

            for (int w = minW; w <= maxW; w++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            Position4d pos = new Position4d(x, y, z, w );
                            yield return new KeyValuePair<Position4d, TValue>(pos, this[pos]);
                        }
                    }
                }
            }
        }

    }
}
