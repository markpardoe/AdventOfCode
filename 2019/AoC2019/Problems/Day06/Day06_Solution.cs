using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.AoC2019.Problems.Day06
{
    public class Day06_Solution : ISolution
    {
        public int Year => 2019;

        public int Day => 6;

        public string Name => "Day 6: Universal Orbit Map";

        public string InputFileName => "Day06.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return new OrbitMap(input).CountOrbits().ToString();
            yield return new OrbitMap(input).FindPath("YOU", "SAN").ToString();
        }
    }
}
