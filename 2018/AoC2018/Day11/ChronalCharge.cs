using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AoC.Common;
using AoC.Common.Mapping;

namespace Aoc.Aoc2018.Day11
{
    public class ChronalCharge :AoCSolution<string>
    {
        public override int Year => 2018;
        public override int Day => 11;
        public override string Name => "Day 11: Chronal Charge";
        public override string InputFileName => null;

        private const int Input = 7165;
        private readonly int GridSize = 300;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            FuelGrid grid = new FuelGrid(Input, GridSize);

            var part1 =  grid.FindLargestPowerRegion(3);
            yield return $"{part1.Position.X},{part1.Position.Y}";

            var part2 = SolvePart2(grid);
            yield return $"{part2.Position.X},{part2.Position.Y},{part2.SearchSize}";
        }

        private SearchResult SolvePart2(FuelGrid map)
        {
            SearchResult max = new SearchResult(new Position(0,0), int.MinValue, 0 );

            for (int searchSize = GridSize; searchSize > 0; searchSize--)
            {
               // Console.WriteLine($"SearchSpace: {searchSize}");
                SearchResult result = map.FindLargestPowerRegion(searchSize);

                if (result.Power > max.Power)
                {
                    max = result;
                }
            }
            return max;
        }
    }
}
