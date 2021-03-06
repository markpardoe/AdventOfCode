﻿using AoC.Common;
using System.Collections.Generic;

namespace Aoc.AoC2019.Problems.Day06
{
    public class Day06_Solution : AoCSolution<int>
    {
        public override int Year => 2019;
        public override int Day => 6;
        public override string Name => "Day 6: Universal Orbit Map";
        public override string InputFileName => "Day06.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return new OrbitMap(input).CountOrbits();
            yield return new OrbitMap(input).FindPath("YOU", "SAN");
        }
    }
}
