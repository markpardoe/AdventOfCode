using System;
using System.Collections.Generic;
using System.Text;

namespace AoC.Common
{
    public abstract class AoCSolution :ISolution
    {
        public string Site => "Advent Of Code";        

        public abstract int Year { get; }

        public abstract int Day { get; }

        public abstract string Name { get; }

        public abstract string InputFileName { get; }

        public string Category => Year.ToString();

        public string SubCategory => $"Day {Day:D2}";

        public string URL => $"https://adventofcode.com/{Year}/{Day}";

        public abstract IEnumerable<string> Solve(IEnumerable<string> input);     
    }
}
