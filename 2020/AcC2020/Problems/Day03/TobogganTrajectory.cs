using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace AoC.AoC2020.Problems.Day03
{
    public class TobogganTrajectory : AoCSolution<long>
    {
        public override int Year => 2020;
        public override int Day => 3;
        public override string Name => "Day 3: Toboggan Trajectory";
        public override string InputFileName => "Day03.txt";

        public override IEnumerable<long> Solve(IEnumerable<string> input)
        {
            TreeMap trees = new TreeMap(input);

            yield return trees.CountTreesOnPath(3, 1);

            long result = 1;

            // Multiply totals for various slopes.
            result *= trees.CountTreesOnPath(1, 1);
            result *= trees.CountTreesOnPath(3, 1);
            result *= trees.CountTreesOnPath(5, 1);
            result *= trees.CountTreesOnPath(7, 1);
            result *= trees.CountTreesOnPath(1, 2);
            yield return result;
        }
    }
}
