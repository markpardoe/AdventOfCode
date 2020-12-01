using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Aoc2018.Day02
{
    public class Day02_Solution : AoCSolution<string>
    {
        public override int Year => 2018;

        public override int Day =>  2;

        public override string Name => "Inventory Management System";

        public override string InputFileName => "Day02.txt";

        public override IEnumerable<string> Solve(IEnumerable<string> input)
        {
            yield return CalculateCheckSum(input).ToString();
            yield return FindSimilarBoxIds(input.ToList());
        }

        public int CalculateCheckSum(IEnumerable<string> input)
        {
            int twoCount = 0;
            int threeCount = 0;
            foreach (string boxId in input)
            {
                var letterGroups = boxId.GroupBy(l => l.ToString())
                    .Select(group => new KeyValuePair<string, int>(group.Key, group.Count()));
                  
               
                if (letterGroups.Any(g => g.Value == 2))
                {
                    twoCount++;
                }
                if (letterGroups.Any(g => g.Value == 3))
                {
                    threeCount++;
                }
            }

            return twoCount * threeCount;
        }

        public string FindSimilarBoxIds(List<string> input)
        {
            for (int i =0;i<input.Count;i++)
            {
                for (int j = i+1; j < input.Count; j++)
                {
                    int diff = CompareWords(input[i], input[j]);
                    if (diff == 1)
                    {
                        return FindSameLetters(input[i], input[j]);
                    }
                }
            }

            return null;
        }

        private string FindSameLetters(string s1, string s2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].Equals(s2[i]))
                {
                    sb.Append(s1[i]);
                }
            }

            return sb.ToString();
        }

        private int CompareWords(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                throw new ArgumentException("Strings must be the same length.");
            }

            int diff = 0;
            for (int i=0;i<s1.Length;i++)
            {
                if (s1[i] != s2[i])
                {
                    diff++;
                }
            }
            return diff;

        }

    }
}
