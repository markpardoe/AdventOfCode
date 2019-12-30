using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;

namespace Aoc.Aoc2018.Day01
{
    public class Day01_Solution : ISolution
    {
        public int Year => 2018;

        public int Day =>  1;

        public string Name => "Chronal Calibration";

        public string InputFileName => "Day01.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
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
