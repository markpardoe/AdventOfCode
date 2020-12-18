using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;
using AoC.Common.Mapping._3d;

namespace AoC.AoC2020.Problems.Day17
{
    public enum CubeStatus
    {
        Active = '#',
        Inactive = '.'
    }

    public class ConwayCubeMap : Map3d<CubeStatus>
    {
        public int Generation { get; private set; } = 0;

        public ConwayCubeMap(IEnumerable<string> input) : base(CubeStatus.Inactive)
        {
            var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {

                    var tile = (CubeStatus)line[x];
                    Position3d pos = new Position3d(x, y, 0);

                    Add(pos, tile);
                }
                y++;
            }
        }

        protected override char? ConvertValueToChar(Position3d position, CubeStatus value) => (char) value;

        public void RunGeneration(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                // Cache min / max values for efficiency
                // We want to work 1 layer outside current map to see if it grows
                int minZ = MinZ - 1;
                int maxZ = MaxZ + 1;
                int minY = MinY - 1;
                int maxY = MaxY + 1;
                int minX = MinX - 1;
                int maxX = MaxX + 1;

                foreach (var location in GetBoundedEnumerator(1))
                {
                    var pos = location.Key;
                    var status = location.Value;
                    int active = CountActiveNeighbors(pos); // count active neighbors

                    // We only add 'Active' cubes to the buffer as this is much more efficient than adding 'inactive' values
                    if (status == CubeStatus.Active && (active == 2 || active == 3))
                    {
                        AddToBuffer(pos, CubeStatus.Active);
                    }
                    else if (status == CubeStatus.Inactive && active == 3)
                    {
                        AddToBuffer(pos, CubeStatus.Active);
                    }
                }

                UpdateFromBuffer(); 
                Generation++;
            }
        }
        
        private int CountActiveNeighbors(Position3d position)
        {
            int count = 0;
            var neighbors = position.GetNeighbors();

            foreach (var pos in neighbors)
            {
                if (this[pos] == CubeStatus.Active)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
