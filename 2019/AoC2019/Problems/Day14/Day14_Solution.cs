using System;
using System.Collections.Generic;
using System.Text;
using AoC.Common;

namespace AoC2019.Problems.Day14
{
    public class Day14_Solution :ISolution
    {
        public int Year => 2019;

        public int Day => 14;

        public string Name => "Day 14: Space Stoichiometry";

        public string InputFileName => "Day14.txt";


        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            NanoReactor nano = new NanoReactor(input);
            long targetOre = 1000000000000;

            yield return nano.ProduceChemical("FUEL", 1).ToString();
            yield return nano.FindFuelOutput(targetOre).ToString();
        }       
    }
}
