using AoC.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.AoC2019
{
    public abstract class AoC2019Solution : ISolution
    {
        public string ParentCategory => "Advent Of Code";

        public virtual string URL => @"https://adventofcode.com/2019";

        public int Year => 2019;

        public abstract  int Day { get; }

        public abstract string Name { get; }

        public abstract string InputFileName { get; }              

        public abstract IEnumerable<string> Solve(IEnumerable<string> input);
    }
}
