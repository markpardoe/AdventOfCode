using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AoC.AoC2020.Problems.Day17
{
    public class Map3d <TValue> 
    {
        protected readonly TValue _default;
        protected Dictionary<Position3d, TValue> _map = new Dictionary<Position3d, TValue>();
        
        public int MaxX => _map.Keys.Max(p => p.X);
        public int MinX => _map.Keys.Min(p => p.X);

        public int MaxY => _map.Keys.Max(p => p.Y);
        public int MinY => _map.Keys.Min(p => p.Y);

        public int MaxZ => _map.Keys.Max(p => p.Z);
        public int MinZ => _map.Keys.Min(p => p.Z);

        public Map3d(TValue defaultValue)
        {
            _default = defaultValue;
            MapConverter = new Func<TValue, char?>(DefaultStringFunction);
        }

        public void Add(Position3d key, TValue value)
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

        public void Add(int x, int y, int z, TValue value) => Add(new Position3d(x, y, z), value);

        public  TValue this[Position3d position]
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

        public TValue this[int x, int y, int z]
        {
            get => this[new Position3d(x, y, z)];
            set => this[new Position3d(x, y, z)] = value;
        }

        public int CountValue(TValue item) => _map.Values.Count(v => v.Equals(item));

        protected Func<TValue, char?> MapConverter;  // function to use for converting the value for mapping purposes
        private char? DefaultStringFunction(TValue value) => value?.ToString()[0];

       

        public virtual string DrawMap()
        {
            int minZ = MinZ;
            int maxZ = MaxZ;
            int minY = MinY;
            int maxY = MaxY;
            int minX = MinX;
            int maxX = MaxX;
            StringBuilder sb = new StringBuilder();

            for (int z = minZ; z <= maxZ; z++)
            {
                sb.Append(Environment.NewLine);
                sb.AppendLine($"z = {z}");

                for (int y = minY; y <= maxY ; y++)
                {
                    for (int x = minX ; x <= maxX ; x++)
                    {
                        sb.Append(MapConverter(this[x, y, z]));
                    }
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        // Returns all positions within the region of the map (between min and max bounds)
        // Will return positions within bounds that are not keys in the map collection
        public IEnumerable<KeyValuePair<Position3d, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            int minZ = MinZ - padding;
            int maxZ = MaxZ + padding;
            int minY = MinY - padding;
            int maxY = MaxY + padding;
            int minX = MinX - padding;
            int maxX = MaxX + padding;

            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        Position3d pos = new Position3d(x,y,z);
                        yield return new KeyValuePair<Position3d, TValue>(pos, this[pos]);
                    }
                }
            }
        }

    }
}
