using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Aoc2018.Day01
{
    public class Day01_Solution : AoC2018Solution
    {
        public override string URL => @"https://adventofcode.com/2018/day/1";

        public override int Day =>  1;

        public override string Name => "Chronal Calibration";

        public override string InputFileName => "Day01.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return CalculateFrequency(input).ToString();
            yield return FindDuplicateFrequency(input).ToString();
        }

        public int CalculateFrequency(IEnumerable<string> input)
        {
            int frequency = 0;

            foreach (string change in input)
            {
                frequency += Int32.Parse(change);
            }

            return frequency;
        }

        public int FindDuplicateFrequency(IEnumerable<string> input)
        {
            HashSet<int> frequencies = new HashSet<int>() { 0 };
            var changes = input.Select(s => Int32.Parse(s));
            int frequency = 0;

            while (true) 
            {               
                foreach (int change in changes)
                {
                    frequency += change;

                    if (frequencies.Contains(frequency))
                    {
                        return frequency;
                    }
                    frequencies.Add(frequency);

                }
            }
        }

    }
}
