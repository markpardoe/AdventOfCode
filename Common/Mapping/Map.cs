using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.Mapping
{
    /// <summary>
    /// Map of 2d Positions
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Map<TValue> : BufferedMap<Position, TValue>
    {
        public virtual int MaxX => Map.Keys.Max(p => p.X);
        public virtual int MinX => Map.Keys.Min(p => p.X);
        public virtual int MaxY => Map.Keys.Max(p => p.Y);
        public virtual int MinY => Map.Keys.Min(p => p.Y);

        // Function to use for converting the value to a char for mapping purposes
        // Override for custom mapping
        protected virtual char? ConvertValueToChar(Position position, TValue value) => value?.ToString()?[0];

        public Map(TValue defaultValue) : base(defaultValue)
        { }

        
        public void Add(int x, int y, TValue value) => Add(new Position(x, y), value);
        
        public TValue this[int x, int y]
        {
            get => this[new Position(x, y)];
            set => this[new Position(x, y)] = value;
        }

        
        public override IEnumerable<Position> GetAvailableNeighbors(Position position)
        {
            return position.GetNeighboringPositions();
        }

        public override string DrawMap()
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
        public override IEnumerable<KeyValuePair<Position, TValue>> GetBoundedEnumerator(int padding = 0)
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