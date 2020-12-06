using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoC.Common;
using AoC.Common.DataStructures;

namespace AoC.AoC2020.Problems.Day06
{
    public class CustomCustoms : AoCSolution<int>
    {
        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            int totalAnyAnswer = 0; // answer for part A
            int totalAllAnswers = 0; // answer for part B
            var group = new List<string>();

            // Process input file
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line) && group.Count > 0)
                {
                    // Empty line indicates a new group.
                    // So get total answers for previous group - then start a new grouping.
                    var answers = CountAnswers(group);
                    totalAnyAnswer += answers.Keys.Count;
                    totalAllAnswers += answers.Values.Count(v => v == group.Count);

                    group = new List<string>();
                }
                else
                {
                    group.Add(line);
                }
            }

            if (group.Count > 0)
            {
                var answers = CountAnswers(group);
                totalAnyAnswer += answers.Keys.Count;
                totalAllAnswers += answers.Values.Count(v => v == group.Count);
            }

            yield return totalAnyAnswer;
            yield return totalAllAnswers;
        }

        public override int Year => 2020;
        public override int Day => 6;
        public override string Name => "Day 6: Custom Customs";
        public override string InputFileName => "Day06.txt";

        public ItemCounter<char> CountAnswers(IEnumerable<string> data)
        {
            var answers = new ItemCounter<char>();
            int lineCount = 0;
            foreach (string line in data)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    lineCount++;
                    foreach (char c in line)
                    {
                        answers.Add(c);
                    }
                }
            }
            return answers;
        }
    }
}