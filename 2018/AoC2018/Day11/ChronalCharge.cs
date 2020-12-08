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
        private const int Input = 7165;
        private readonly int GridSize = 300;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            FuelGrid grid = new FuelGrid(Input, GridSize);

          //  yield return grid.DrawMap();
          //  yield return grid.RowTotals.DrawMap();

            yield return grid.FindLargestPowerRegion(3).ToString();
           // yield return grid.FindLargestPowerRegion2(3).ToString();

           var watch = new Stopwatch();
           watch.Start();
            yield return SolvePart2(grid).ToString();
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds / 1000} s");
        }

        public override int Year => 2018;
        public override int Day => 11;
        public override string Name => "Day 11: Chronal Charge";
        public override string InputFileName => null;


        private SearchResult SolvePart2(FuelGrid map)
        {
            SearchResult max = new SearchResult(new Position(0,0), int.MinValue, 0 );

            for (int searchSize = GridSize; searchSize >0; searchSize--)
            {
                Console.WriteLine($"SearchSpace: {searchSize}");
                SearchResult result = map.FindLargestPowerRegion2(searchSize);

                if (result.Power > max.Power)
                {
                    max = result;
                }
            }

            return max;
        }
    }
}
