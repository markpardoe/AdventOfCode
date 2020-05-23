using AoC.Common;
using System.Collections.Generic;

namespace Aoc.Aoc2018
{
    public abstract class AoC2018Solution : ISolution
    {
        public string ParentCategory => "Advent Of Code";

        public virtual string URL => @"https://adventofcode.com/2018";

        public int Year => 2018;

        public abstract  int Day { get; }

        public abstract string Name { get; }

        public abstract string InputFileName { get; }              

        public abstract IEnumerable<string> Solve(IEnumerable<string> input);
    }
}
