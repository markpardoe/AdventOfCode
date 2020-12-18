using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.Mapping
{
    public class Map<TValue>
    {
        public virtual int MaxX => _map.Keys.Max(p => p.X);
        public virtual int MinX => _map.Keys.Min(p => p.X);
        public virtual int MaxY => _map.Keys.Max(p => p.Y);
        public virtual int MinY => _map.Keys.Min(p => p.Y);

        protected readonly TValue DefaultValue;
        protected int DrawPadding { get; set; } = 0;    // Empty spaces to include when drawing the map

        protected Dictionary<Position, TValue> _map = new Dictionary<Position, TValue>();
        
        public Map(TValue defaultValueValue)
        {
            DefaultValue = defaultValueValue;
        }

        // Function to use for converting the value to a char for mapping purposes
        // Override for custom mapping
        protected virtual char? ConvertValueToChar(Position position, TValue value) => value?.ToString()?[0];

        public virtual void Add(Position key, TValue value) => AddOrReplace(key, value);
  
        public void Add(int x, int y, TValue value) => Add(new Position(x, y), value);

        public virtual TValue this[Position position]
        {
            get
            {
                if (!_map.ContainsKey(position))
                {
                    return DefaultValue;
                }
                else
                {
                    return _map[position];
                }
            }
            set => AddOrReplace(position, value);
        }
        
        public TValue this[int x, int y]
        {
            get => this[new Position(x, y)];
            set => this[new Position(x, y)] = value;
        }

        protected virtual void AddOrReplace(Position key, TValue value)
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

       public int CountValue(TValue item) => _map.Values.Count(v => v.Equals(item));

        public virtual IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions();
        }

        public virtual string DrawMap()
        {
            int minY = MinY - DrawPadding;
            int maxY = MaxY + DrawPadding;
            int minX = MinX - DrawPadding;
            int maxX = MaxX + DrawPadding;
            StringBuilder sb = new StringBuilder();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Position pos = new Position(x, y);
                    sb.Append(ConvertValueToChar(pos, this[pos]));
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        // Returns all positions within the region of the map (between min and max bounds)
        // Will return positions within bounds that are not keys in the map collection
        public IEnumerable<KeyValuePair<Position, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            int minY = MinY - padding;
            int maxY = MaxY + padding;
            int minX = MinX - padding;
            int maxX = MaxX + padding;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Position pos = new Position(x, y);
                    yield return new KeyValuePair<Position, TValue>(pos, this[pos]);
                }
            }
        }
    }
}