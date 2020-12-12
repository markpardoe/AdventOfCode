using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day17
{

    public class ReservoirResearch : AoCSolution<int>
    {

        public override int Year => 2018;
        public override int Day => 17;
        public override string Name => "Day 17: Reservoir Research";
        public override string InputFileName => "Day17.txt";

        private Position _spring = new Position(500,0);

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            var floodMap = new FloodMap(input, _spring);
            //  DrawMap(floodMap);

            floodMap.PourWater(_spring);

            Console.WriteLine(floodMap.DrawMap());

            yield return floodMap.WaterTiles;
            yield return floodMap.RestingWaterTiles;
        }

        

        private readonly List<string> example1 = new List<string>()
        {
            "x=495, y=2..7",
            "y=7, x=495..501",
            "x=501, y=3..7",
            "x=498, y=2..4",
            "x=506, y=1..2",
            "x=498, y=10..13",
            "x=504, y=10..13",
            "y=13, x=498..504"
        };

    }
}
