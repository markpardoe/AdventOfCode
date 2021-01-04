using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.Mapping.Hex
{
    public class HexGrid<TValue> : BufferedMap<AxialPosition, TValue>
    {
        public virtual int MaxX => Map.Keys.Max(p => p.X);
        public virtual int MinX => Map.Keys.Min(p => p.X);

        public virtual int MaxY => Map.Keys.Max(p => p.Y);
        public virtual int MinY => Map.Keys.Min(p => p.Y);

        public virtual int MaxZ => Map.Keys.Max(p => p.Z);
        public virtual int MinZ => Map.Keys.Min(p => p.Z);

        public HexGrid(TValue defaultValue) : base(defaultValue)
        {
        }

        public void Add(int x, int y, int z, TValue value) => Add(new AxialPosition(x, y, z), value);

        public TValue this[int x, int y, int z]
        {
            get => this[new AxialPosition(x, y, z)];
            set => this[new AxialPosition(x, y, z)] = value;
        }

        protected override IEnumerable<AxialPosition> GetNeighboringPositions(AxialPosition position)
        {
            return position.GetNeighbors();
        }

        public override string DrawMap(int padding = 0)
        {
            int minY = MinY;
            int maxY = MaxY;
            int minX = MinX;
            int maxX = MaxX; 
            
            StringBuilder sb = new StringBuilder();


            for (int y = minY; y <= maxY; y++)
            {
                sb.Append(Environment.NewLine);
                if (y % 2 == 0)
                {
                    sb.Append(" ");  // offset alternative lines
                }

                for (int x = minX; x <= maxX; x++)
                {
                    if (x == 0 && y == 0)
                    {
                        sb.Append("X ");
                    }
                    else
                    {
                        int z = 0 - x - y;
                        AxialPosition pos = new AxialPosition(x, y, z);

                        sb.Append(ConvertValueToChar(pos, this[pos]));
                        sb.Append(" ");
                    }
                }
            }
            

            return sb.ToString();
        }

        public override IEnumerable<KeyValuePair<AxialPosition, TValue>> GetBoundedEnumerator(int padding = 0)
        {
            int minY = MinY - padding;
            int maxY = MaxY + padding;
            int minX = MinX - padding;
            int maxX = MaxX + padding;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    int z = 0 - x - y;
                    AxialPosition pos = new AxialPosition(x, y, z);

                    yield return new KeyValuePair<AxialPosition, TValue>(pos, this[pos]);
                }
            }
        }

        // Function to use for converting the value to a char for mapping purposes
        // Override for custom mapping
        protected virtual char? ConvertValueToChar(AxialPosition position, TValue value) => value?.ToString()?[0];
    }
}
