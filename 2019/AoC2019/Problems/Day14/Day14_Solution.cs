using System.Collections.Generic;

namespace Aoc.AoC2019.Problems.Day14
{
    public class Day14_Solution :AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/14";

        public override int Day => 14;

        public override string Name => "Day 14: Space Stoichiometry";

        public override string InputFileName => "Day14.txt";


        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            NanoReactor nano = new NanoReactor(input);
            long targetOre = 1000000000000;

            yield return nano.ProduceChemical("FUEL", 1).ToString();
            yield return nano.FindFuelOutput(targetOre).ToString();
        }       
    }
}
