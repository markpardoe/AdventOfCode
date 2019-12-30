using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day05
{
    public class Day05_Solution : ISolution
    {
        public int Year => 2018;

        public int Day => 5;

        public string Name => "Day 5: Alchemical Reduction";

        public string InputFileName => "Day05.txt";

        public IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return ReducePolymer(input.First()).Length.ToString();
            yield return FindShortestWithReduction(input.First()).ToString();
        }

        public string ReducePolymer(string polymer)
        {
            List<char> units = polymer.ToList();

            while (true)
            {
                bool matchFound = false;
                for (int ix = 0; ix < units.Count - 1; ix++)
                {
                    char current = units[ix];
                    char next = units[ix + 1];

                    if (current - next == 32 || next - current == 32)
                    {
                        units.RemoveAt(ix);
                        units.RemoveAt(ix);
                        matchFound = true;
                    }
                }

                if (!matchFound)
                {
                    return string.Join("", units);
                }
            }
        }

        public int FindShortestWithReduction(string polymer)
        {
            var toCheck = polymer.ToUpper().Select(c => c.ToString().ToUpper()).Distinct();
            int minLen = Int32.MaxValue;

            foreach (string s in toCheck)
            {
                string temp = polymer.Replace(s, "").Replace(s.ToLower(), "");
                int len = ReducePolymer(temp).Length;

                if (len < minLen)
                {
                    minLen = len;
                }
            }

            return minLen;
        }
    }
}
