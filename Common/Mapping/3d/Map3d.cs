using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.Common.Mapping._3d
{
    /// <summary>
    /// 3d Map
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Map3d <TValue> : BufferedMap<Position3d, TValue>
    {
        
        public int MaxX => Map.Keys.Max(p => p.X);
        public int MinX => Map.Keys.Min(p => p.X);

        public int MaxY => Map.Keys.Max(p => p.Y);
        public int MinY => Map.Keys.Min(p => p.Y);

        public int MaxZ => Map.Keys.Max(p => p.Z);
        public int MinZ => Map.Keys.Min(p => p.Z);

        public Map3d(TValue defaultValue) : base(defaultValue) {}
        
      
        public void Add(int x, int y, int z, TValue value) => Add(new Position3d(x, y, z), value);

        public TValue this[int x, int y, int z]
        {
            get => this[new Position3d(x, y, z)];
            set => this[new Position3d(x, y, z)] = value;
        }

        // Function to use for converting the value to a char for mapping purposes
        // Override for custom mapping
        protected virtual char? ConvertValueToChar(Position3d position, TValue value) => value?.ToString()?[0];

        protected override IEnumerable<Position3d> GetAvailableNeighbors(Position3d position)
        {
            return position.GetNeighbors();
        }

        public override string DrawMap()
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
                        Position3d pos = new Position3d(x, y, z);
                        sb.Append(ConvertValueToChar(pos, this[pos]));
                    }
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        // Returns all positions within the region of the map (between min and max bounds)
        // Will return positions within bounds that are not keys in the map collection
        public override IEnumerable<KeyValuePair<Position3d, TValue>> GetBoundedEnumerator(int padding = 0)
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
