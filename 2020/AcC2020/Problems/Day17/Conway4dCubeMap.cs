using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.AoC2020.Problems.Day17
{

    public class Conway4dCubeMap : Map4d<CubeStatus>
    {
        public int Generation { get; private set; } = 0;

        private readonly Dictionary<Position4d, CubeStatus> _buffer = new Dictionary<Position4d, CubeStatus>();

        public Conway4dCubeMap(IEnumerable<string> input) : base(CubeStatus.Inactive)
        {
            MapConverter = new Func<CubeStatus, char?>(EnumChar);  // Set the converter to use for drawing

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
                        _buffer[pos] = CubeStatus.Active;
                    }
                    else if (status == CubeStatus.Inactive && active == 3)
                    {
                        _buffer[pos] = CubeStatus.Active;
                    }
                }
                
                // replace the map with the buffer
                _map.Clear();

                foreach (var location in _buffer)
                {
                    // only copy active locations
                    if (location.Value == CubeStatus.Active)
                    {
                        _map.Add(location.Key, location.Value);
                    }
                }

                _buffer.Clear();
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
     
        // Convert the enum to its char value for mapping purposes
        private char? EnumChar(CubeStatus value)
        {
            return (char) value;
        }
    }
}
