using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day20
{
    public class RegularMap : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
          //  string regEx = input.First();  // shpuld only be one line

            yield return 5;
            yield return 5;
        }

        public override int Year => 2018;
        public override int Day => 20;
        public override string Name => "Day 20: A Regular Map";
        public override string InputFileName => "Day20.txt";
    }
}
