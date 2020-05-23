using AoC.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day01
{ 
    
    public class Day01_Solution : ISolution
    {
        public string URL => @"https://adventofcode.com/2019/day/1";

        public int Year => 2019;

        public int Day => 1;

        public string Name => "Day 1: The Tyranny of the Rocket Equation";

        public string InputFileName => "Day01.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return CalculateFuelRequirements(input, false).ToString();
            yield return CalculateFuelRequirements(input, true).ToString();
        }

        public int CalculateFuelRequirements(IEnumerable<string> data, bool recursive = false)
        {
            int total = 0;
            var intData = data.Select(s => Int32.Parse(s));

            foreach (int i in intData)
            {
                if (recursive)
                {
                    total += CalculateFuelRecursive(i);
                }
                else
                {
                    total += CalculateFuel(i);
                }
            }
            return total;
        }

        private int CalculateFuel(int mass)
        {
            int fuel = (mass / 3) - 2;
            return fuel;
        }

        private int CalculateFuelRecursive(int mass)
        {
            int fuel = (mass / 3) - 2;
            if (fuel <= 0)
            {
                return 0;
            }
            else
            {
                return fuel + CalculateFuelRecursive(fuel);
            }
        }
    }    
}
