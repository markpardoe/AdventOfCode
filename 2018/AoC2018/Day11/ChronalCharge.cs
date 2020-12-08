using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day11
{
    public class ChronalCharge :AoCSolution<string>
    {
        private const int Input = 7165;
        private readonly int GridSize = 300;

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            FuelGrid grid = new FuelGrid(Input, GridSize);
            yield return grid.FindLargestPowerRegion().ToString();
        }

        public override int Year => 2018;
        public override int Day => 11;
        public override string Name => "Day 11: Chronal Charge";
        public override string InputFileName => null;
    }
}
