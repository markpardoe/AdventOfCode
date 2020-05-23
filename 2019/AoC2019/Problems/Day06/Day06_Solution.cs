using System.Collections.Generic;

namespace Aoc.AoC2019.Problems.Day06
{
    public class Day06_Solution : AoC2019Solution
    {
        public override string URL => @"https://adventofcode.com/2019/day/6";
        public override int Day => 6;
        public override string Name => "Day 6: Universal Orbit Map";
        public override string InputFileName => "Day06.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return new OrbitMap(input).CountOrbits().ToString();
            yield return new OrbitMap(input).FindPath("YOU", "SAN").ToString();
        }
    }
}
