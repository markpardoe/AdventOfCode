using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day20
{
    public class RegularMap : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            string data = input.First();
            RoomMap map = new RoomMap(data);

            // Get paths (including distances) to all rooms in the maze
            var paths = map.FindPathToAllValues(map.Start, MapTile.Room);
            yield return paths.Max(x => x.DistanceFromStart);
            yield return paths.Count(x => x.DistanceFromStart >= 1000);
        }

        public override int Year => 2018;
        public override int Day => 20;
        public override string Name => "Day 20: A Regular Map";
        public override string InputFileName => "Day20.txt";
    }
}
