using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common.Mapping;
using AoC.Common.Mapping._4d;

namespace AoC.AoC2020.Problems.Day17
{

    public sealed class Conway4dCubeMap : Map4d<CubeStatus>
    {
        public int Generation { get; private set; } = 0;

        public Conway4dCubeMap(IEnumerable<string> input) : base(CubeStatus.Inactive)
        {
            var y = 0;
            foreach (var line in input)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    var tile = (CubeStatus)line[x];
                    var pos = new Position4d(x, y, 0, 0);

                    Add(pos, tile);
                }
                y++;
            }
        }

        public void RunGeneration(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                // We want to work 1 layer outside current map to see if it grows
                foreach (var location in GetBoundedEnumerator(1))
                {
                    var pos = location.Key;
                    var status = location.Value;
                    int active = CountActiveNeighbors(pos); // count active neighbors

                    if (status == CubeStatus.Active && (active == 2 || active == 3))
                    {
                        AddToBuffer(pos, CubeStatus.Active);
    }
                    else if (status == CubeStatus.Inactive && active == 3)
                    {
                        AddToBuffer(pos, CubeStatus.Active);
                    }
                }
                
                // replace the map with the buffer
                UpdateFromBuffer();
                Generation++;
            }
        }

       

        private int CountActiveNeighbors(Position4d position)
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
