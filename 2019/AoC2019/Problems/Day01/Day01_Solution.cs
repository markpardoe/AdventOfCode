﻿using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.AoC2019.Problems.Day01
{

    public class Day01_Solution : AoCSolution<int>
    {
        public override int Year => 2019;

        public override int Day => 1;

        public override string Name => "Day 1: The Tyranny of the Rocket Equation";

        public override string InputFileName => "Day01.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            yield return CalculateFuelRequirements(input, false);
            yield return CalculateFuelRequirements(input, true);
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
