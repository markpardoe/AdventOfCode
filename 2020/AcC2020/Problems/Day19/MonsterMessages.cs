using AoC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.AoC2020.Problems.Day19
{
    public class MonsterMessages : AoCSolution<int>
    {
        public override int Year => 2020;
        public override int Day => 19;
        public override string Name => "Day 19: Monster Messages";
        public override string InputFileName => "Day19.txt";

        public override IEnumerable<int> Solve(IEnumerable<string> input)
        {
            throw new NotImplementedException();
        }


        private void ParseRules(List<string> input)
        {
            

        }
    }


    public class Rule
    {
        public int RuleId { get; }
        public bool IsLetter { get; private set; } = false;
        public char Letter { get; set; }

        // Or rules
        public readonly List<List<int>> OrRules = new List<List<int>>();

        public Rule (string input)
        {
            var split = input.Split(":", StringSplitOptions.RemoveEmptyEntries);

            RuleId = int.Parse(split[0].Trim());

            var ruleText = split[1].Trim();
            if (ruleText.Contains('"'))  //its a letter
            {
                Letter = ruleText[1];
            }
            else
            {
                var orRules = ruleText.Split('|');

                foreach (var rule in orRules)
                {
                    OrRules.Add(rule.Trim().Split(' ').Select(x => int.Parse(x)).ToList());
                }
            }
        }
    }
}