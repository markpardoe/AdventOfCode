using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.Common.Mapping._4d
{
    public class Map4d <TValue> : MapBase<Position4d, TValue>
    {
        
        public int MaxX => Map.Keys.Max(p => p.X);
        public int MinX => Map.Keys.Min(p => p.X);

        public int MaxY => Map.Keys.Max(p => p.Y);
        public int MinY => Map.Keys.Min(p => p.Y);

        public int MaxZ => Map.Keys.Max(p => p.Z);
        public int MinZ => Map.Keys.Min(p => p.Z);

        public int MaxW => Map.Keys.Max(p => p.W);
        public int MinW => Map.Keys.Min(p => p.W);

        public Map4d(TValue defaultValue) : base(defaultValue) { }


        public void Add(int x, int y, int z, int w, TValue value) => Add(new Position4d(x, y, z, w), value);

        public TValue this[int x, int y, int z, int w]
        {
            get => this[new Position4d(x, y, z, w)];
            set => this[new Position4d(x, y, z, w)] = value;
        }

        public override string DrawMap()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Position4d> GetAvailableNeighbors(Position4d position)
        {
            return position.GetNeighbors();
        }

        // Returns all positions within the region of the map (between min and max bounds)
        // Will return positions within bounds that are not keys in the map collection
        public override IEnumerable<KeyValuePair<Position4d, TValue>> GetBoundedEnumerator(int padding = 0)
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